using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebManager.DBContexts;
using WebManager.DataTransferObjects;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace WebManager.Controllers
{
    [Route("api/[controller]")]
    public class LogoutController : Controller
    {
        public LogoutController()
        {
        }

        [HttpGet("[action]")]
        public void Logout()
        {
            AuthenticationHttpContextExtensions.SignOutAsync(HttpContext, CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
