using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebManager.Model
{
    public class Group
    {
        [Key]
        public int Id { get; set; }
        public string GroupName { get; set; }
        public virtual ICollection<Task> Tasks { get; } = new List<Task>();
        public virtual ICollection<UserGroup> UsersGroups { get; } = new List<UserGroup>();
    }
}
