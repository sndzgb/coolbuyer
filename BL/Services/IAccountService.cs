using CoolBuyer.Server.BusinessLogic.Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CoolBuyer.Server.BusinessLogic.Services
{
    public interface IAccountService
    {
        string Login(string username, string password);
        void Register(NewApplicationUserDTO model);
        void ChangePassword(int userId, string oldPassword, string newPassword);
        void ConfirmAccountByEmail(string token, int userId);
    }
}
