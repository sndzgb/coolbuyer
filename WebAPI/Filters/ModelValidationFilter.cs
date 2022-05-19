using CoolBuyer.Server.WebApi.Extensions;
using CoolBuyer.Server.WebApi.Results;
using CoolBuyer.Server.WebApi.WebApiModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace CoolBuyer.Server.WebApi.Filters
{
    public class ModelValidationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var modelState = actionContext.ModelState;

            if (!modelState.IsValid)
            {
                var errorResponse = new ErrorResponseWebApiModel();
                errorResponse.ErrorMessage = "One or more model errors were encountered.";
                errorResponse.StatusCode = (int)HttpStatusCode.BadRequest;

                errorResponse.Errors = modelState.GetModelErrors();

                //var y = modelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors });
                //foreach (var e in y)
                //{
                //    var lastDotIndex = e.Key.LastIndexOf('.');
                //    var key = e.Key.Remove(0, lastDotIndex + 1);
                //    errorResponse.Errors.Add(Char.ToLower(key[0]) + key.Substring(1), e.Errors[0].ErrorMessage);
                //}

                actionContext.Response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(errorResponse), Encoding.UTF8, "application/json")
                };
            }
        }
    }
}