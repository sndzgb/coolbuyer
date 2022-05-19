using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace CoolBuyer.Server.WebApi.Security.Principal
{
    public class CoolBuyerPrincipal : ICoolBuyerPrincipal
    {
        public CoolBuyerPrincipal()
        {
            this.Identity = new GenericIdentity("");
        }

        public CoolBuyerPrincipal(int id, string username, string email)
        {
            this.Identity = new GenericIdentity(email);

            this.Email = email;
            this.Id = id;
            this.Username = username;
        }
        
        public string Email { get; private set; }
        public string Username { get; private set; }
        public int Id { get; private set; }

        public IIdentity Identity { get; private set; }
        public bool IsInRole(string role) { return false; }
    }
}