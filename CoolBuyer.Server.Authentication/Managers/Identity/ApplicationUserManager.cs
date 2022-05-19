using CoolBuyer.Server.BusinessLogic.Common.Managers;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoolBuyer.Server.BusinessLogic.Common.Models;
using System.Security.Claims;
using CoolBuyer.Server.BusinessLogic.Common.DTOs;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using CoolBuyer.Server.BusinessLogic.Common.Exceptions;
using CoolBuyer.Server.BusinessLogic.Common.Security.DataProtection;
using Newtonsoft.Json;
using CoolBuyer.Server.BusinessLogic.Services;

namespace CoolBuyer.Server.Authentication.Managers.Identity
{
    public class ApplicationUserManager : UserManager<ApplicationUser, int>, IUserManager
    {
        private readonly ISymmetricCryptographyService symmetricCryptoService;


        public ApplicationUserManager(
            IUserStore<ApplicationUser, int> store,
            ISymmetricCryptographyService symmetricCryptoService
        )
            : base(store)
        {
            this.symmetricCryptoService = symmetricCryptoService;
        }
        
        public string GenerateProtectedToken(string purpose, int userId)
        {
            var token = symmetricCryptoService.Encrypt(
                JsonConvert.SerializeObject(new ProtectedToken()
                {
                    ExpiresAtUTC = DateTime.UtcNow.AddHours(12),
                    IssuedToUserId = userId,
                    Purpose = purpose
                })
            );

            return token;
        }

        public bool ValidateProtectedToken(string purpose, string token, int userId)
        {
            bool isValid = true;

            var protectedToken = JsonConvert.DeserializeObject<ProtectedToken>
                (
                    symmetricCryptoService.Decrypt(token)
                );

            if (protectedToken.ExpiresAtUTC < DateTime.UtcNow)
            {
                return isValid = false;
            }

            if (purpose != protectedToken.Purpose)
            {
                return isValid = false;
            }

            if (userId != protectedToken.IssuedToUserId)
            {
                return isValid = false;
            }

            return isValid;
        }
        
        public void ConfirmAccount(string token, int userId)
        {
            if (!ValidateProtectedToken("ConfirmAccount", token, userId))
            {
                throw new AccountConfirmationFailedException("Invalid confirmation token.");
            }

            var user = Task.Run(async () => await base.FindByIdAsync(userId)).Result;

            if (user.EmailConfirmed)
            {
                return;
            }

            user.EmailConfirmed = true;
            var result = Task.Run(async () => await base.UpdateAsync(user)).Result;
        }

        public string GenerateEmailAccountConfirmationTokenAsync(int userId)
        {
            return GenerateProtectedToken("ConfirmAccount", userId);
        }

        public void ChangePassword(int userId, string oldPassword, string newPassword)
        {
            Task.Run(async () => await base.ChangePasswordAsync(userId, oldPassword, newPassword));
        }

        public void CreateUser(NewApplicationUserDTO newUser, out int userId)
        {
            var user = new ApplicationUser(newUser.Username, newUser.Password, newUser.Email);

            ValidateUser(user);

            if (UsernameExists(newUser.Username))
            {
                throw new CreateNewUserException("Username is already taken.");
            }

            if (EmailExists(newUser.Email))
            {
                throw new CreateNewUserException("Error occured while creating your account.");
            }

            var result = Task.Run(async () => await base.CreateAsync(user, newUser.Password)).Result;

            if (!result.Succeeded)
            {
                throw new CreateNewUserException("There was an error creating new user.");
            }

            userId = user.Id;
        }
        
        public ApplicationUserDTO FindUser(int userId)
        {
            var user = Task.Run(async () => await base.FindByIdAsync(userId)).Result;
            return new ApplicationUserDTO()
            {
                IsLockedOut = user.LockoutEnabled,
                Email = user.Email,
                Id = user.Id,
                EmailConfirmed = user.EmailConfirmed,
                Username = user.UserName,
                RegistrationDate = user.DateRegistered,
                LockoutEndDateUTC = user.LockoutEndDateUtc
            };
        }

        public ApplicationUserDTO FindUser(string username, string password)
        {
            var user = Task.Run(async () => await base.FindAsync(username, password)).Result;
            return new ApplicationUserDTO()
            {
                Email = user.Email,
                Id = user.Id,
                EmailConfirmed = user.EmailConfirmed,
                Username = user.UserName,
                RegistrationDate = user.DateRegistered,
                IsLockedOut = user.LockoutEnabled
            };
        }
        

        private bool EmailExists(string email)
        {
            return Task.Run(async () => await base.FindByEmailAsync(email)).Result != null;
        }

        private bool UsernameExists(string username)
        {
            return Task.Run(async () => await base.FindByNameAsync(username)).Result != null;
        }

        private void ValidateUser(ApplicationUser user)
        {
            var validationContext = new ValidationContext(user, serviceProvider: null, items: null);
            Validator.ValidateObject(user, validationContext, true);
        }
    }


    class ProtectedToken
    {
        public ProtectedToken()
        {
            this.IssuedAtUTC = DateTime.UtcNow;
        }

        public int IssuedToUserId { get; set; }
        public DateTime ExpiresAtUTC { get; set; }
        public DateTime IssuedAtUTC { get; private set; }
        public string Purpose { get; set; }
    }
}
