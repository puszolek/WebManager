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
    public class TasksController : Controller
    {
        public DatabaseContext Context { get; set; }

        public TasksController(DatabaseContext context)
        {
            Context = context;
        }

        [HttpGet("[action]")]
        public IEnumerable<TasksDisplayDto> TasksDisplays()
        {
            var userEmail = IdentityHelper.GetUserEmail(User);
            var data = Context.Users.Include(cc => cc.UsersGroups)
                                .ThenInclude(bb => bb.Group)
                                .ThenInclude(aa => aa.Tasks)
                                .FirstOrDefault(x => x.Email == userEmail);

            var tasks = data?.UsersGroups
                            .Select(u => u.Group)
                            .SelectMany(g => g.Tasks);

            return tasks.Select(task => new TasksDisplayDto
            {
                DueDate = task.DueDate,
                Title = task.Title,
                Details = task.Details,
                CreationDate = task.CreationDate,
                Group = task.Group.GroupName
            });
        }
    }
}
