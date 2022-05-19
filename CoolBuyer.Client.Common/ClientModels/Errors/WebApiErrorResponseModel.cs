using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolBuyer.Client.Common.ClientModels.Errors
{
    /// <summary>
    /// Describes what caused the error when making an web api call.
    /// </summary>
    public class WebApiErrorResponseModel
    {
        public WebApiErrorResponseModel()
        {
            this.Errors = new Dictionary<string, string>();
        }
        
        public string ErrorMessage { get; set; }
        public int StatusCode { get; set; }
        public Dictionary<string, string> Errors { get; set; }
    }
}
