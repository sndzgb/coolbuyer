using CoolBuyer.Client.Common.ClientExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoolBuyer.Client.Web.MVC.Filters
{
    /// <summary>
    /// Validates the ModelState of the posted model.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            if (filterContext.Controller.ViewData.ModelState.IsValid == false)
            {
                filterContext.Result = new ViewResult()
                {
                    ViewData = filterContext.Controller.ViewData,
                    TempData = filterContext.Controller.TempData
                };

                return;
            }
        }
    }
}