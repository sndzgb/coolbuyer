using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CoolBuyer.Server.Authentication.Managers.Identity
{
    public class ApplicationUser : IdentityUser<int, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {
        public ApplicationUser()
        {

        }

        public ApplicationUser(string userName, string password, string email, string phoneNumber = "")
        {
            this.DateRegistered = DateTime.UtcNow;
            this.UserName = userName;
            this.Email = email;
            this.PhoneNumber = phoneNumber;
            
            ValidateUser(this);
        }

        /// <summary>
        /// Validates the new user information.
        /// </summary>
        /// <param name="applicationUser"></param>
        private void ValidateUser(ApplicationUser applicationUser)
        {
        }
        
        public DateTime DateRegistered { get; private set; }
    }
}
