using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebManager.DBContexts;
using WebManager.DataTransferObjects;
using System.Security.Claims;
using System.Security.Principal;

namespace WebManager.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        public UsersContext UsersDataContext { get; set; }

        public TasksContext Context { get; set; }

        public SampleDataController(TasksContext context, UsersContext usersContext)
        {
            Context = context;
            UsersDataContext = usersContext;
        }

        [HttpGet("[action]")]
        public IEnumerable<TasksDisplayDto> TasksDisplays()
        {
            var userEmail = IdentityHelper.GetUserEmail(User);
            var t = Context.Tasks;
            var u = UsersDataContext.Users.FirstOrDefault(x => x.Email == userEmail)?.Groups.Split(" ");

            return t.Where(task => u.Contains(task.Group)).Select(task => new TasksDisplayDto
            {
                DueDate = task.DueDate,
                Title = task.Title,
                Details = task.Details,
                CreationDate = task.CreationDate,
                Group = task.Group
            });
        }
    }
}
