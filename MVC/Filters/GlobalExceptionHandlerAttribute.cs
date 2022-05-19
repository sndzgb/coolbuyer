using CoolBuyer.Client.Common.ClientExceptions;
using CoolBuyer.Client.Common.ClientModels.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CoolBuyer.Client.Web.MVC.Filters
{
    /// <summary>
    /// Handles exceptions.
    /// </summary>
    public class GlobalExceptionHandlerAttribute : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled)
            {
                if (filterContext.Exception is ApiCallException)
                {
                    var ex = filterContext.Exception as ApiCallException;

                    var apiException = filterContext.Exception as ApiCallException;

                    if (filterContext.HttpContext.Request.IsAjaxRequest())
                    {
                        HandleAjaxCallError(filterContext);
                        return;
                    }

                    if (apiException.StatusCode == 400)
                    {
                        var modelState = (Dictionary<string, object>)filterContext.HttpContext.Items["ActionParameters"];
                        string key = modelState.Keys.FirstOrDefault();
                        var requestModel = modelState[key];
                        
                        var controllerName = (string)filterContext.RouteData.Values["controller"];
                        var actionName = (string)filterContext.RouteData.Values["action"];

                        filterContext.Result = new ViewResult
                        {
                            ViewName = actionName,
                            ViewData = new ViewDataDictionary<object>(requestModel)
                            {
                                new KeyValuePair<string, object>("ErrorMessage", apiException.ErrorMessage),
                                new KeyValuePair<string, object>("Errors", apiException.Errors)
                            },
                            TempData = filterContext.Controller.TempData
                        };
                        filterContext.RequestContext.HttpContext.Response.StatusCode = 400;
                        filterContext.ExceptionHandled = true;
                        return;
                    }
                    if (apiException.StatusCode == 401)
                    {
                        filterContext.Result = new RedirectToRouteResult(
                            new RouteValueDictionary
                            {
                                { "controller", "account" },
                                { "action", "index" },
                                { "returnUrl", filterContext.HttpContext.Request.RawUrl }
                            });
                        filterContext.ExceptionHandled = true;
                        return;
                    }
                    if (apiException.StatusCode == 403)
                    {
                        filterContext.Result = new ViewResult
                        {
                            ViewName = "~/Views/Error/Forbidden.cshtml",
                            ViewData = new ViewDataDictionary<WebApiErrorResponseModel>()
                            {
                                new KeyValuePair<string, object>("ErrorMessage", ex.ErrorMessage),
                                new KeyValuePair<string, object>("StatusCode", ex.StatusCode),
                                new KeyValuePair<string, object>("Errors", ex.Errors)
                            }
                        };
                        filterContext.ExceptionHandled = true;
                        return;
                    }
                    if (apiException.StatusCode == 404)
                    {
                        filterContext.Result = new ViewResult
                        {
                            ViewName = "~/Views/Error/NotFound.cshtml",
                            ViewData = new ViewDataDictionary<WebApiErrorResponseModel>()
                            {
                                new KeyValuePair<string, object>("ErrorMessage", ex.ErrorMessage),
                                new KeyValuePair<string, object>("StatusCode", ex.StatusCode),
                                new KeyValuePair<string, object>("Errors", ex.Errors)
                            }
                        };
                        filterContext.ExceptionHandled = true;
                        return;
                    }
                    if (apiException.StatusCode == 500)
                    {
                        filterContext.Result = new ViewResult
                        {
                            ViewName = "~/Views/Error/InternalServerError.cshtml",
                            ViewData = new ViewDataDictionary<WebApiErrorResponseModel>()
                            {
                                new KeyValuePair<string, object>("ErrorMessage", ex.ErrorMessage),
                                new KeyValuePair<string, object>("StatusCode", ex.StatusCode),
                                new KeyValuePair<string, object>("Errors", ex.Errors)
                            }
                        };
                        filterContext.ExceptionHandled = true;
                        return;
                    }
                }

                HandleLocalException(filterContext);

                #region default handler
                if (!filterContext.ExceptionHandled)
                {
                    filterContext.RequestContext.HttpContext.Response.StatusCode = 500;
                    filterContext.Result = new ViewResult
                    {
                        ViewName = "~/Views/Error/InternalServerError.cshtml"
                    };
                    filterContext.ExceptionHandled = true;
                }
                #endregion
            }
        }

        private void HandleLocalException(ExceptionContext filterContext)
        {
            var exceptionType = filterContext.GetType();

            if (exceptionType == typeof(TimeoutException))
            {
                filterContext.ExceptionHandled = true;
                filterContext.Result = new ViewResult
                {
                    ViewName = "~/Views/Error/RequestTimeout.cshtml"
                };
                return;
            }

            if (filterContext.Exception.Message == "The operation was canceled." || exceptionType == typeof(OperationCanceledException))
            {
                filterContext.ExceptionHandled = true;
                filterContext.Result = new ViewResult
                {
                    ViewName = "~/Views/Error/OperationCanceled.cshtml"
                };
                return;
            }

            if (exceptionType == typeof(TaskCanceledException))
            {
                filterContext.ExceptionHandled = true;
                filterContext.Result = new ViewResult
                {
                    ViewName = "~/Views/Error/TaskCanceled.cshtml"
                };
                return;
            }

            if (exceptionType == typeof(HttpAntiForgeryException))
            {
                filterContext.ExceptionHandled = true;
                filterContext.HttpContext.Response.StatusCode = 400;
                filterContext.Result = new ViewResult
                {
                    ViewName = "~/Views/Error/BadRequest.cshtml"
                };
                return;
            }

            if (filterContext.Exception.Message.Contains("its master was not found or no view engine supports the searched locations. The following locations were searched"))
            {
                filterContext.ExceptionHandled = true;
                filterContext.HttpContext.Response.StatusCode = 500;
                filterContext.Result = new ViewResult
                {
                    ViewName = "~/Views/Error/InternalServerError.cshtml"
                };
                return;
            }
        }

        /// <summary>
        /// Handles ajax call exception.
        /// </summary>
        /// <param name="filterContext"></param>
        private void HandleAjaxCallError(ExceptionContext filterContext)
        {
            var apiException = filterContext.Exception as ApiCallException;

            filterContext.HttpContext.Response.StatusCode = apiException.StatusCode;
            filterContext.Result = new JsonResult()
            {
                Data = new WebApiErrorResponseModel()
                {
                    StatusCode = apiException.StatusCode,
                    ErrorMessage = apiException.ErrorMessage,
                    Errors = apiException.Errors
                },
                ContentType = "application/json"
            };

            filterContext.ExceptionHandled = true;
        }
    }
}