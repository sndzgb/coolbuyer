using CoolBuyer.Server.WebApi.WebApiModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace CoolBuyer.Server.WebApi.Handlers.MessageHandlers
{
    public class RequireHttpsHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.RequestUri.Scheme != Uri.UriSchemeHttps)
            {
                return Task.FromResult(new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden)
                {
                    ReasonPhrase = "HTTP not available.",
                    Content = new StringContent(JsonConvert.SerializeObject(new ErrorResponseWebApiModel() { ErrorMessage = "Http not available", StatusCode = 400, Errors = null }), Encoding.UTF8, "application/json")
                });
            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}