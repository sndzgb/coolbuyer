using CoolBuyer.Client.Common.ClientModels.CBPrincipal;
using CoolBuyer.Client.Common.ClientModels.Products;
using CoolBuyer.Client.Web.MVC.App_Start;
using CoolBuyer.Client.Web.MVC.Controllers.ControllerFactory;
using CoolBuyer.Client.Web.MVC.ObjectModelBinders;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace CoolBuyer.Client.Web.MVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            ControllerBuilder.Current.SetControllerFactory(typeof(CoolBuyerControllerFactory));
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            DIContainerBuilder.Build();
            ModelBinders.Binders.Add(typeof(NewProductViewModel), new CreateProductModelBinder());
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            // last resort
            // log all uncaught errors here & return some response

            Exception exception = Server.GetLastError();
            
            System.Diagnostics.Debug.WriteLine(exception);
            
            var ex = Server.GetLastError();
            Server.ClearError();
            Response.TrySkipIisCustomErrors = true;
            Response.Redirect("~/Views/Error/NotFound.cshtml");
            Response.Clear();
        }

        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
        }

        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            var authenticationCookieName = ConfigurationManager.AppSettings["AuthenticationCookieName"];
            var authenticationCookie = HttpContext.Current.Request.Cookies[authenticationCookieName];

            HttpContext.Current.User = new CoolBuyerPrincipal(authenticationCookie == null ? "" : authenticationCookie.Value);
            Thread.CurrentPrincipal = new CoolBuyerPrincipal(authenticationCookie == null ? "" : authenticationCookie.Value);
        }
    }
}
