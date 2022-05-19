using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoolBuyer.Server.WebApi.WebApiModels.Authentication
{
    public class LoginDetailsWebApiModel
    {
        public LoginDetailsWebApiModel()
        {

        }

        //public LoginDetailsWebApiModel(string username, string password)
        //{
        //    this.Username = username;
        //    this.Password = password;
        //}

        public string Username { get; set; }
        public string Password { get; set; }
    }
}