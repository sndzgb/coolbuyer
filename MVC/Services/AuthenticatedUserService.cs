using CoolBuyer.Client.Common.ClientModels.CBPrincipal;
using CoolBuyer.Client.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoolBuyer.Client.Web.MVC.Services
{
    /// <summary>
    /// Keeps track of authenticated user information.
    /// </summary>
    public class AuthenticatedUserService : IAuthenticatedUserService
    {
        public int Id
        {
            get
            {
                return ((ICoolBuyerPrincipal)HttpContext.Current.User).Id;
            }
        }

        /// <summary>
        /// Token which is used to identify a user on the server.
        /// </summary>
        public string Token
        {
            get
            {
                return ((ICoolBuyerPrincipal)HttpContext.Current.User).EncodedToken;
            }
        }

        public string Username
        {
            get
            {
                return ((ICoolBuyerPrincipal)HttpContext.Current.User).Username;
            }
        }
    }
}