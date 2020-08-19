using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FactoryDesignPattern.Migration;
using FactoryDesignPattern.Models;
using FactoryDesignPattern.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace FactoryDesignPattern
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;

      //Run migrations
      var migrationHelper = new MigrationHelpers();
      migrationHelper.UpdateDatabase(configuration.GetConnectionString("Default"));
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddControllersWithViews();

      services.AddDbContextPool<AppDbContext>(options =>
          options.UseSqlServer(Configuration.GetConnectionString("Default")));

      services.AddAuthentication(options =>
          {
              options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
              options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
              options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
              options.DefaultSignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;
          })
          .AddCookie(options =>
          {
              options.Cookie.Name = "practice_cookie";
              options.Cookie.HttpOnly = true;
              options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
              options.AccessDeniedPath = new PathString("/forbidden");
              options.LoginPath = new PathString("/account/login");
              options.ExpireTimeSpan = TimeSpan.FromDays(15);
              options.SlidingExpiration = true;
          });

      services.AddTransient<IEmployeeRepository, SqlEmployeeRepository>();
      services.AddTransient<ISecurityService, SecurityService>();
      services.AddTransient<IUserRepository, SqlUserRepository>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
      }
      app.UseHttpsRedirection();
      app.UseStaticFiles();

      app.UseRouting();

      app.UseAuthentication();
      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllerRoute(
                  name: "default",
                  pattern: "{controller=Home}/{action=Index}/{id?}");
      });
    }
  }
}
