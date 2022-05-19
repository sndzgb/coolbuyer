using CoolBuyer.Server.WebApi.Extensions;
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
using System.Web.Http.ModelBinding;

namespace CoolBuyer.Server.WebApi.Results
{
    public class InvalidModelStateResult : IHttpActionResult
    {
        public ModelStateDictionary ModelState { get; private set; }
        public HttpRequestMessage Request { get; private set; }
        public string ReasonPhrase { get; private set; }


        public InvalidModelStateResult(ModelStateDictionary modelState, HttpRequestMessage request, string reasonPhrase = "")
        {
            this.ModelState = modelState;
            this.ReasonPhrase = reasonPhrase.Trim().Length == 0 ? "One or more model errors were encountered." : reasonPhrase;
            this.Request = request;
        }


        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute());
        }

        private HttpResponseMessage Execute()
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.BadRequest);
            response.RequestMessage = Request;
            response.ReasonPhrase = ReasonPhrase;

            var errorResponse = new ErrorResponseWebApiModel();
            errorResponse.ErrorMessage = ReasonPhrase;
            errorResponse.StatusCode = (int)response.StatusCode;

            var errors = ModelState.GetModelErrors();
            errorResponse.Errors = errors;

            response.Content = new StringContent(
                JsonConvert.SerializeObject(errorResponse), 
                Encoding.UTF8, 
                "application/json"
            );

            return response;
        }
    }
}