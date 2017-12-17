using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebManager.DBContexts;
using WebManager.DataTransferObjects;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.EntityFrameworkCore;


namespace WebManager.Controllers
{
    [Route("api/[controller]")]
    public class GroupsController : Controller
    {
        public DatabaseContext Context { get; set; }

        public GroupsController(DatabaseContext context)
        {
            Context = context;
        }

        [HttpGet("[action]")]
        public IEnumerable<GroupDto> GetCurrentUsersGroups()
        {
            var userEmail = IdentityHelper.GetUserEmail(User);
            var data = Context
                        .Groups
                        .Include(g => g.UsersGroups)
                        .ThenInclude(ug => ug.User)
                        .Where(g => g.UsersGroups
                            .Any(ug => ug.User.Email == userEmail))
                        .ToList();

            return data.Select(g => new GroupDto
            {
                Id = g.Id,
                Name = g.GroupName
            }).ToList();
        }
    }
}
