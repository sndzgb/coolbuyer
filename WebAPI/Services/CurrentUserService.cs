using CoolBuyer.Server.BusinessLogic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoolBuyer.Server.WebApi.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private int GetUserId()
        {
            var user = (Security.Principal.ICoolBuyerPrincipal)HttpContext.Current.User;
            var id = user.Id;
            return id;
        }

        public int Id
        {
            get
            {
                var id = GetUserId();
                return id == 0 ? -1 : id;
            }
        }
    }
}