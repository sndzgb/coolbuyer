using CoolBuyer.Server.BusinessLogic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoolBuyer.Server.BusinessLogic.Common.DTOs;
using CoolBuyer.Server.BusinessLogic.Common.Managers;
using System.Security.Claims;
using CoolBuyer.Server.BusinessLogic.Common.Exceptions;
using System.Net;

namespace CoolBuyer.Server.Authentication.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserManager userManager;
        private readonly IEmailService emailService;
        private readonly ITokenGeneratorService tokenGeneratorService;


        public AccountService(
                IUserManager userManager, 
                IEmailService emailService, 
                ITokenGeneratorService tokenGeneratorService
            )
        {
            this.userManager = userManager;
            this.emailService = emailService;
            this.tokenGeneratorService = tokenGeneratorService;
        }
        

        public string Login(string username, string password)
        {
            ApplicationUserDTO user = userManager.FindUser(username, password);

            if (user == null)
            {
                throw new NotFoundException("User with specified combination of username and password was not found.");
            }

            if (!user.EmailConfirmed)
            {
                throw new AccountNotConfirmedException("Please confirm your account before logging in.");
            }

            if (user.IsLockedOut)
            {
                throw new UnauthorizedException("Not authorized to login.");
            }

            var claims = new Claim[]
            {
                    new Claim("Id", user.Id.ToString()),
                    new Claim("Username", user.Username),
                    new Claim("Email", user.Email)
            };

            return tokenGeneratorService.GenerateToken(claims);
        }

        public void ChangePassword(int userId, string oldPassword, string newPassword)
        {
            userManager.ChangePassword(userId, oldPassword, newPassword);
        }

        public void ConfirmAccountByEmail(string token, int userId)
        {
            var uToken = WebUtility.UrlDecode(token);
            var fToken = uToken.Replace(" ", "+");

            userManager.ConfirmAccount(fToken, userId);
        }
        
        public void Register(NewApplicationUserDTO model)
        {
            int userId;
            userManager.CreateUser(model, out userId);

            var token = userManager.GenerateEmailAccountConfirmationTokenAsync(userId);
            var encodedUrl = WebUtility.UrlEncode(token);
            var confirmationTokenUri = $"https://localhost:44301/account/confirm-account?token={encodedUrl}&userId={userId}";
            
            emailService.Send(new EmailMessage()
            {
                Body = $"Please confirm your account by clicking on the provided <a href=\"{confirmationTokenUri}\">link</a>.",
                FromAddress = "noreply@domain.com",
                Subject = "Account confirmation - CoolBuyer",
                ToAddress = model.Email
            });
        }
    }
}
