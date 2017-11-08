using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebManager.DataTransferObjects
{
    public class RegisterUserDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
        public string Email { get; set; }
    }
}
