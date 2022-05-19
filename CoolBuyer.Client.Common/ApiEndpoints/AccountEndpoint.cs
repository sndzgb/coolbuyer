using CoolBuyer.Client.Common.ApiEndpoints.EndpointsAbstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoolBuyer.Client.Common.ClientModels;
using CoolBuyer.Client.Common.ApiClient;
using System.Net.Http;
using Newtonsoft.Json;
using CoolBuyer.Client.Common.ClientModels.Errors;
using CoolBuyer.Client.Common.ClientExceptions;
using CoolBuyer.Client.Common.ClientModels.Account;

namespace CoolBuyer.Client.Common.ApiEndpoints
{
    public class AccountEndpoint : IAccountEndpoint
    {
        private readonly ICoolBuyerHttpClient client;


        public AccountEndpoint(ICoolBuyerHttpClient client)
        {
            this.client = client;
        }


        public async Task<AuthenticatedUserViewModel> LoginAsync(UserLoginViewModel model)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Post, "api/account/login"))
            {
                var payload = JsonConvert.SerializeObject(model);

                using (var stringContent = new StringContent(payload, Encoding.UTF8, "application/json"))
                {
                    request.Content = stringContent;
                    
                    using (var response = await client.ApiClient.SendAsync(request))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            var authUser = new AuthenticatedUserViewModel(content);
                            return authUser;
                        }
                        else
                        {
                            var json = await response.Content.ReadAsStringAsync();
                            var errorModel = JsonConvert.DeserializeObject<WebApiErrorResponseModel>(json);
                            throw new ApiCallException(errorModel.ErrorMessage, errorModel.StatusCode, errorModel.Errors);
                        }
                    }
                }
            }
        }

        public async Task ConfirmAccountAsync(string confirmationToken, int userId)
        {
            var uri = $"/api/account/confirm-account?token={confirmationToken}&userId={userId}";

            using (var request = new HttpRequestMessage(HttpMethod.Post, uri))
            {
                using (var response = await client.ApiClient.SendAsync(request))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return;
                    }
                    else
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        var errorModel = JsonConvert.DeserializeObject<WebApiErrorResponseModel>(json);
                        throw new ApiCallException(errorModel.ErrorMessage, errorModel.StatusCode, errorModel.Errors);
                    }
                }
            }
        }

        public async Task RegisterAsync(RegistrationDetailsViewModel model)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Post, "api/account/register"))
            {
                var payload = JsonConvert.SerializeObject(model);

                using (var stringContent = new StringContent(payload, Encoding.UTF8, "application/json"))
                {
                    request.Content = stringContent;

                    using (var response = await client.ApiClient.SendAsync(request))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            return;
                        }
                        else
                        {
                            var json = await response.Content.ReadAsStringAsync();
                            var errorModel = JsonConvert.DeserializeObject<WebApiErrorResponseModel>(json);
                            throw new ApiCallException(errorModel.ErrorMessage, errorModel.StatusCode, errorModel.Errors);
                        }
                    }
                }
            }
        }
    }
}
