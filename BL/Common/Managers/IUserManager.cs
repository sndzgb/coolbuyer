using CoolBuyer.Server.BusinessLogic.Common.DTOs;
using CoolBuyer.Server.BusinessLogic.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CoolBuyer.Server.BusinessLogic.Common.Managers
{
    public interface IUserManager
    {
        void CreateUser(NewApplicationUserDTO newUser, out int userId);
        ApplicationUserDTO FindUser(string username, string password);
        ApplicationUserDTO FindUser(int userId);
        void ChangePassword(int userId, string oldPassword, string newPassword);
        string GenerateEmailAccountConfirmationTokenAsync(int userId);
        void ConfirmAccount(string token, int userId);

        string GenerateProtectedToken(string purpose, int userId);
        bool ValidateProtectedToken(string purpose, string token, int userId);
    }
}
