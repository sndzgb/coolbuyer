using CoolBuyer.Client.Common.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace CoolBuyer.Client.Common.ClientModels.CBPrincipal
{
    internal class AuthenticationTokenContent
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }

        [JsonProperty("exp")]
        public long ExpirationDateUnix { get; set; }
    }

    public class CoolBuyerPrincipal : ICoolBuyerPrincipal
    {
        public CoolBuyerPrincipal()
        {
            this.Identity = new GenericIdentity(string.Empty);
        }
        
        public CoolBuyerPrincipal(string authenticationToken)
        {
            // extra
            if (authenticationToken == "")
            {
                this.Identity = new GenericIdentity(string.Empty);
                return;
            }

            this.Token = authenticationToken;
            ParseToken(authenticationToken);

            if (Token == "")
            {
                this.Identity = new GenericIdentity(string.Empty);
            }
            else
            {
                // if token expired, don't set principal
                if (ExpirationDateUTC < DateTime.UtcNow)
                {
                    this.Identity = new GenericIdentity(string.Empty);
                }
                else
                {
                    this.Identity = new GenericIdentity(this.Username);
                }
            }
        }
        private void ParseToken(string token)
        {
            var content = token.Substring(token.IndexOf('.') + 1, token.LastIndexOf('.') - (token.IndexOf('.') + 1));
            var base64EncodedBytes = Convert.FromBase64String(content);
            var decodedString = Encoding.UTF8.GetString(base64EncodedBytes);
            var plainToken = JsonConvert.DeserializeObject<AuthenticationTokenContent>(decodedString);

            Id = plainToken.Id;
            Username = plainToken.Username;
            Email = plainToken.Email;
            ExpirationDateUTC = DateTimeFormatter.FromUnixTime(plainToken.ExpirationDateUnix);
            EncodedToken = token;
        }

        //public int Id { get; private set; }
        public string Token { get; private set; }
        //public string Username { get; private set; }
        public string Email { get; private set; }
        public DateTime ExpirationDateUTC { get; private set; }


        public CoolBuyerPrincipal(string username, int id, string token, string authenticationType)
        {
            this.Username = username;
            this.Id = id;
            this.EncodedToken = token;
            this.Identity = new GenericIdentity(username, authenticationType);
        }


        public int Id { get; private set; }

        public string Username { get; private set; }

        public IIdentity Identity { get; private set; }

        public string EncodedToken { get; private set; }        

        //public DateTime TokenExpirationDateUTC { get; private set; }

        public bool IsInRole(string role)
        {
            return false;
        }
    }
}
