using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq;
using WebManager.DataTransferObjects;
using WebManager.DBContexts;

namespace WebApplication1.Controllers
{
    public class AccountController : Controller
    {
        public UsersContext Context { get; set; }

        public AccountController(UsersContext context)
        {
            Context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUserDto user)
        {
            // wybieramy usera z bazy, jesli istnieje to cisniemy
            //var context = new UsersContext();
            var context = Context;
            // to get all users do: context.Users
            //var d = context.Users.Find(1);
            var d = context.Users.ToList();
            var e = context.Users.Where(x => x.Email == user.Email).FirstOrDefault();
            var userExists = e != null;

            if(user.Email == null || user.Password == null)
            {
                userExists = false;
            }

            if (userExists)
            {
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name,"username usera z bazy"),
                    new Claim(ClaimTypes.Email,user.Email),
                    new Claim(ClaimTypes.Role, "User")
                };

                var userPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, "SuperSecureLogin"));
                await AuthenticationHttpContextExtensions.SignInAsync(HttpContext, 
                                                                    CookieAuthenticationDefaults.AuthenticationScheme, 
                                                                    userPrincipal, 
                                                                    new AuthenticationProperties
                                                                    {
                                                                        ExpiresUtc = DateTime.UtcNow.AddMinutes(30),
                                                                        IsPersistent = false,
                                                                        AllowRefresh = false
                                                                    });

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.ErrMsg = "UserName or Password is invalid! Please try again.";

                return View();
            }
        }

        [HttpPost]
        public IActionResult Register(RegisterUserDto user)
        {
            if (user.Password.Equals(user.PasswordConfirmation, StringComparison.Ordinal))
            {
                //dodajemy usera do bazy jesli takiego nie ma

                return RedirectToAction("Login", "Account");
            }
            else
            {
                ViewBag.ErrMsg = "Passwords are different!";

                return View();
            }
        }

        public async Task<IActionResult> Logout()
        {
            await AuthenticationHttpContextExtensions.SignOutAsync(HttpContext, CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }
    }
}
