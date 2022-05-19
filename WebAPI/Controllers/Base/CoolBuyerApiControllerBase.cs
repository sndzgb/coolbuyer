using CoolBuyer.Server.WebApi.Security.Principal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace CoolBuyer.Server.WebApi.Controllers.Base
{
    public class CoolBuyerApiControllerBase : ApiController
    {
        protected virtual new CoolBuyerPrincipal User
        {
            get
            {
                return HttpContext.Current.User as CoolBuyerPrincipal;
            }
        }
    }
}