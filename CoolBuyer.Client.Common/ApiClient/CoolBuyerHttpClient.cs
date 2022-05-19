using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CoolBuyer.Client.Common.ApiClient
{
    // NOT DONE: inject baseAddress to this instance

    public class CoolBuyerHttpClient : ICoolBuyerHttpClient
    {
        private static HttpClient _apiClient;
        public HttpClient ApiClient
        {
            get
            {
                return _apiClient;
            }
        }


        public CoolBuyerHttpClient(string uriBaseAddress = "")
        {
            ConfigureHttpClient();
        }
                

        private void ConfigureHttpClient()
        {
            //ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            ServicePointManager.ServerCertificateValidationCallback =
                delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                };
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12;

            string baseAddress = "https://localhost:44300";

            _apiClient = new HttpClient();

            _apiClient.BaseAddress = new Uri(baseAddress);

            _apiClient.DefaultRequestHeaders.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
