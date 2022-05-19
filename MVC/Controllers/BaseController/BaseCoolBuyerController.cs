using CoolBuyer.Client.Common.ClientModels.CBPrincipal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CoolBuyer.Client.Web.MVC.Controllers.BaseController
{
    public abstract class BaseCoolBuyerController : Controller
    {
        
        protected virtual new CoolBuyerPrincipal User
        {
            get
            {
                return HttpContext.User as CoolBuyerPrincipal;
            }
        }

        public ActionResult InvokeHttp404(HttpContextBase httpContext)
        {
            IController errorController = new ErrorController();
            var errorRoute = new RouteData();
            errorRoute.Values.Add("controller", "Error");
            errorRoute.Values.Add("action", "PageNotFound");
            errorRoute.Values.Add("url", httpContext.Request.Url.OriginalString);
            errorController.Execute(new RequestContext(
                 httpContext, errorRoute));

            return new EmptyResult();
        }

        protected override void HandleUnknownAction(string actionName)
        {
            if (this.GetType() != typeof(ErrorController))
                this.InvokeHttp404(HttpContext);
            
        }
        
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.HttpContext.Items["ActionParameters"] = filterContext.ActionParameters;
            base.OnActionExecuting(filterContext);
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            // if exception gets to here, log it
            filterContext.ExceptionHandled = true;

            var exception = filterContext.Exception;
            //_Logger.Error(filterContext.Exception);
        }
    }
}
