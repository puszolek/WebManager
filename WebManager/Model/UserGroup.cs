using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebManager.Model
{
    [Table("UsersGroups")]
    public class UserGroup
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int GroupId { get; set; }
        public virtual Group Group { get; set; }
    }
}
