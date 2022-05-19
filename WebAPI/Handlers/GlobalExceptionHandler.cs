using CoolBuyer.Server.BusinessLogic.Common.Exceptions;
using CoolBuyer.Server.WebApi.WebApiModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;

namespace CoolBuyer.Server.WebApi.Handlers
{
    public class GlobalExceptionHandler : ExceptionHandler
    {
        // this should return 500
        // this should check for invalid route
        // this should log the exception

        public override void Handle(ExceptionHandlerContext context)
        {
            // get logger from DI container
            //_logger.Log();
            
            context.Result = new JsonErrorResult
            {
                Request = context.ExceptionContext.Request,
                Content = new ErrorResponseWebApiModel() { ErrorMessage = "Oops! Something went wrong on the server.", StatusCode = 500, Errors = null }
            };
        }

        private class JsonErrorResult : IHttpActionResult
        {
            public HttpRequestMessage Request { get; set; }

            public ErrorResponseWebApiModel Content { get; set; }

            public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                response.Content = new StringContent(JsonConvert.SerializeObject(Content), Encoding.UTF8, "application/json");
                response.RequestMessage = Request;
                return Task.FromResult(response);
            }
        }
    }
}