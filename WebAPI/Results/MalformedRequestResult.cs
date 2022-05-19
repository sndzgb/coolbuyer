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

namespace CoolBuyer.Server.WebApi.Results
{
    public class MalformedRequestResult : IHttpActionResult
    {
        public HttpRequestMessage Request { get; private set; }
        public string ReasonPhrase { get; private set; }


        public MalformedRequestResult(HttpRequestMessage requestMessage, string reasonPhrase)
        {
            this.Request = requestMessage;
            this.ReasonPhrase = reasonPhrase;
        }


        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute());
        }

        private HttpResponseMessage Execute()
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.BadRequest);
            response.RequestMessage = Request;

            var errorResponse = new ErrorResponseWebApiModel();
            errorResponse.ErrorMessage = ReasonPhrase;
            errorResponse.StatusCode = (int)response.StatusCode;

            response.Content = new StringContent(
                JsonConvert.SerializeObject(errorResponse),
                Encoding.UTF8,
                "application/json"
            );

            return response;
        }
    }
}