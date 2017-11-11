using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebManager.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        private static string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [HttpGet("[action]")]
        public IEnumerable<TasksDisplay> TasksDisplays()
        {
            var rng = new Random();

            return Enumerable.Range(1, 6).Select(index => new TasksDisplay
            {
                DueDate = DateTime.Now.AddDays(index).ToString("d"),
                Title = Summaries[rng.Next(Summaries.Length)],
                Details = Summaries[rng.Next(Summaries.Length)],
                CreationDate = DateTime.Now.ToString("d"),
                Users = Summaries[rng.Next(Summaries.Length)]
            });
        }

        public class TasksDisplay
        {
            public string DueDate { get; set; }
            public string Title { get; set; }
            public string Details { get; set; }
            public string CreationDate { get; set; }
            public string Users { get; set; }
        }
    }
}
