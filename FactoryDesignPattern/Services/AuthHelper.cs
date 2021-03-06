﻿using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using FactoryDesignPattern.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace FactoryDesignPattern.Services
{
    public static class AuthHelper
    {
        public static async Task SetAuthenticationCookie(HttpContext httpContext, User user, bool isPersistent,
            string authIssuer)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email, ClaimValueTypes.Email)
            };

            var userIdentity = new ClaimsIdentity("MySecureLogin");
            userIdentity.AddClaims(claims);

            var userPrincipal = new ClaimsPrincipal(userIdentity);

            var authenticationProperties = new AuthenticationProperties
            {
                IsPersistent = isPersistent
            };

            await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                userPrincipal,
                authenticationProperties
            );

            httpContext.User = userPrincipal;
        }
    }
}