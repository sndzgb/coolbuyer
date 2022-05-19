using CoolBuyer.Client.Common.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolBuyer.Client.Common.ClientModels.Account
{
    public class AuthenticatedUserViewModel
    {
        public AuthenticatedUserViewModel(string token)
        {
            this.Token = token;
            ParseToken(token);
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
        }

        public int Id { get; private set; }
        public string Token { get; private set; }
        public string Username { get; private set; }
        public string Email { get; private set; }
        public DateTime ExpirationDateUTC { get; private set; }
    }

    internal class AuthenticationTokenContent
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }

        [JsonProperty("exp")]
        public long ExpirationDateUnix { get; set; }
    }
}
