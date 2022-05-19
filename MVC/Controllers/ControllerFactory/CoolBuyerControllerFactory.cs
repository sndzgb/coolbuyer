using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CoolBuyer.Client.Web.MVC.Controllers.ControllerFactory
{
    public class CoolBuyerControllerFactory : DefaultControllerFactory
    {
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            try
            {
                if (controllerType == null)
                    return base.GetControllerInstance(requestContext, controllerType);
                else
                    return base.GetControllerInstance(requestContext, controllerType) as Controller;
            }
            catch (HttpException ex)
            {
                if (ex.GetHttpCode() == (int)HttpStatusCode.NotFound)
                {
                    requestContext.RouteData.Values["controller"] = "Error";
                    requestContext.RouteData.Values["action"] = "PageNotFound";
                    requestContext.RouteData.Values.Add("url", requestContext.HttpContext.Request.Url.OriginalString);

                    return new ErrorController();
                }
                else
                {
                    throw ex;
                }
            }
        }
    }
}