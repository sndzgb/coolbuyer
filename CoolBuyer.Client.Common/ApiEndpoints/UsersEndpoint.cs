using CoolBuyer.Client.Common.ApiEndpoints.EndpointsAbstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoolBuyer.Client.Common.ClientModels.Users;
using CoolBuyer.Client.Common.ApiClient;
using System.Net.Http;
using Newtonsoft.Json;
using CoolBuyer.Client.Common.ClientModels.Errors;
using CoolBuyer.Client.Common.ClientExceptions;

namespace CoolBuyer.Client.Common.ApiEndpoints
{
    public class UsersEndpoint : IUsersEndpoint
    {
        private ICoolBuyerHttpClient httpClient;


        public UsersEndpoint(ICoolBuyerHttpClient httpClient)
        {
            this.httpClient = httpClient;
        }


        public async Task<UserProfileIndexDetailsViewModel> GetSingleAsync(int userId, int take, int page)
        {
            var uri = $"/api/users/{userId}?take={take}&page={page}";

            using (var request = new HttpRequestMessage(HttpMethod.Get, uri))
            {
                using (var response = await httpClient.ApiClient.SendAsync(request))
                {
                    var content = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        var user = JsonConvert.DeserializeObject<UserProfileIndexDetailsViewModel>(content);
                        return user;
                    }
                    else
                    {
                        var errorResponse = JsonConvert.DeserializeObject<WebApiErrorResponseModel>(content);
                        throw new ApiCallException(errorResponse.ErrorMessage, errorResponse.StatusCode, errorResponse.Errors);
                    }
                }
            }
        }
    }
}
