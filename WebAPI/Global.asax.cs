using CoolBuyer.Server.WebApi.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace CoolBuyer.Server.WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            ContainerConfiguration.Configure();
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
