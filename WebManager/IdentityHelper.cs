using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebManager.DBContexts;
using WebManager.DataTransferObjects;
using System.Security.Claims;
using System.Security.Principal;

namespace WebManager
{
    public static class IdentityHelper
    {
        public static string GetUserEmail(ClaimsPrincipal User)
        {
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            var claimsItems = claims.ToArray();
            
            return claimsItems[1].Value;
        }
    }
}
