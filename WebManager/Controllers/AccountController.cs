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
using WebManager.Model;

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
            // to get all users do: context.Users
            // var d = context.Users.Find(1);
            // for debugging purposes: show list of users
            // var d = Context.Users.ToList();

            // fetch user from database
            var fetchedUser = Context.Users.Where(x => x.Email == user.Email).FirstOrDefault();

            // check whether user exists
            var userExists = fetchedUser != null;

            if (userExists)
            {
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name,"username z bazy"),
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
                User newUser = new User();
                newUser.Email = user.Email;
                newUser.Password = user.Password;
                newUser.Username = user.UserName;

                // add user to database
                Context.Users.Add(newUser);
                Context.SaveChanges();

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
