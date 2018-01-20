using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebManager.DBContexts;
using WebManager.DataTransferObjects;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.EntityFrameworkCore;
using WebManager.Model;

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
                Id = task.Id,
                DueDate = task.DueDate,
                Title = task.Title,
                Details = task.Details,
                CreationDate = task.CreationDate,
                Group = task.Group.GroupName
            });
        }

        [HttpPost("[action]")]
        public bool AddTask([FromBody]  NewTaskDto taskDto)
        {
            try
            {
                var group = Context.Groups.Where(g => g.Id == taskDto.GroupId).FirstOrDefault();
                var newTask = new Task
                {
                    Title = taskDto.Title,
                    Details = taskDto.Details,
                    DueDate = taskDto.DueDate,
                    CreationDate = DateTime.Now,
                    Group = group
                };

                Context.Add(newTask);
                Context.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        [HttpPost("[action]")]
        public bool DeleteTask([FromBody] TasksDisplayDto taskDto)
        {
            try
            {
                var taskToDelete = Context.Tasks.Where(g => g.Id == taskDto.Id).FirstOrDefault();
                Context.Entry(taskToDelete).State = EntityState.Deleted;
                Context.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
