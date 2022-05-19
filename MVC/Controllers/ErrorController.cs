using CoolBuyer.Client.Web.MVC.Controllers.BaseController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CoolBuyer.Client.Web.MVC.Controllers
{
    public class ErrorController : BaseCoolBuyerController
    {
        [ActionName("PageNotFound")]
        public ActionResult PageNotFound()
        {
            Response.StatusCode = (int)HttpStatusCode.NotFound;
            Response.TrySkipIisCustomErrors = true;
            //HttpContext.Response.TrySkipIisCustomErrors = true;
            return View("~/Views/Error/PageNotFound.cshtml");
        }

        // 500 etc
        [ActionName("InternalServerError")]
        public ActionResult InternalServerError()
        {
            Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            Response.TrySkipIisCustomErrors = true;
            return View("~/Views/Error/InternalServerError.cshtml");
        }

    }
}
