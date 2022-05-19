using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolBuyer.Server.BusinessLogic.Common.DTOs
{
    public class NewApplicationUserDTO
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        //public string PhoneNumber { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
