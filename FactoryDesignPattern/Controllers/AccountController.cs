using System.Threading.Tasks;
using FactoryDesignPattern.Models;
using FactoryDesignPattern.Services;
using FactoryDesignPattern.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace FactoryDesignPattern.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly ISecurityService securityService;

        public AccountController(IUserRepository userRepository, ISecurityService securityService)
        {
            this.userRepository = userRepository;
            this.securityService = securityService;
        }

        public IActionResult Register()
        {
            return View(new User());
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            userRepository.RegisterUser(user);

            return View(user);
        }

        public IActionResult Login()
        {
            return View(new LoginInput());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginInput input)
        {
            User user = securityService.ValidateCredentials(input.Email, input.Password);
            if (user != null)
            {
                await AuthHelper.SetAuthenticationCookie(HttpContext, user, false, string.Empty);
                return RedirectToAction("Index", "Employees");
            }

            return View(new LoginInput());
        }

        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            foreach (string cookiesKey in Request.Cookies.Keys)
            {
                Response.Cookies.Delete(cookiesKey);
            }
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}