using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoolBuyer.Server.WebApi.WebApiModels.Authentication
{
    public class AuthenticatedUserWebApiModel
    {
        public string Email { get; set; }
        public int Id { get; set; }
        public string Username { get; set; }
        public DateTime RegistrationDate { get; set; }        
        public bool AccountConfirmed { get; set; }

        //public Token Token { get; set; }
    }

    public class Token
    {
        [JsonProperty("expires")]
        public DateTimeOffset TokenExpirationUtcDate { get; set; }
    }
}