using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace CoolBuyer.Server.WebApi.WebApiModels
{
    public class ErrorResponseWebApiModel
    {
        public ErrorResponseWebApiModel()
        {
            this.Errors = new Dictionary<string, object>();
        }

        [JsonProperty("statusCode")]
        public int StatusCode { get; set; }

        [JsonProperty("errorMessage")]
        public string ErrorMessage { get; set; }

        [JsonProperty("errors")]
        public Dictionary<string, object> Errors { get; set; }
    }
}