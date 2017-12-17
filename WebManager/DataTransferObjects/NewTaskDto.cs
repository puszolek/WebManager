using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebManager.DataTransferObjects
{
    public class NewTaskDto
    {
        public DateTime DueDate { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public int GroupId { get; set; }
    }
}
