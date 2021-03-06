﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebManager.Model
{
    public class Task
    {
        [Key]
        public int Id { get; set; }
        public int GroupId { get; set; }
        public virtual Group Group { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime DueDate { get; set; }
    }
}
