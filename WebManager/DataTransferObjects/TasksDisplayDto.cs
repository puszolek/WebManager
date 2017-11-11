using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebManager.DataTransferObjects
{
    public class TasksDisplayDto
    {
        public string Group { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime DueDate { get; set; }
    }
}
