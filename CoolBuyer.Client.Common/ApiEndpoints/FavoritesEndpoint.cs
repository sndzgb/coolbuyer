using CoolBuyer.Client.Common.ApiClient;
using CoolBuyer.Client.Common.ApiEndpoints.EndpointsAbstractions;
using CoolBuyer.Client.Common.ClientExceptions;
using CoolBuyer.Client.Common.ClientModels.Errors;
using CoolBuyer.Client.Common.ClientModels.Favorites;
using CoolBuyer.Client.Common.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CoolBuyer.Client.Common.ApiEndpoints
{
    public class FavoritesEndpoint : IFavoritesEndpoint
    {
        private readonly IAuthenticatedUserService authenticatedUserService;
        private readonly ICoolBuyerHttpClient client;


        public FavoritesEndpoint(ICoolBuyerHttpClient client, IAuthenticatedUserService authenticatedUserService)
        {
            this.client = client;
            this.authenticatedUserService = authenticatedUserService;
        }


        public async Task AddOrRemoveAsync(AddOrRemoveFavoriteViewModel model)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Post, "api/favorites"))
            {
                var payload = JsonConvert.SerializeObject(model);

                using (var content = new StringContent(payload, Encoding.UTF8, "application/json"))
                {
                    request.Content = content;
                    request.Headers.Authorization = new AuthenticationHeaderValue("bearer", authenticatedUserService.Token);

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
        
        public async Task<UserFavoritesIndexViewModel> GetAsync(int take, int page)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, $"api/favorites?take={take}&page={page}"))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("bearer", authenticatedUserService.Token);

                using (var response = await client.ApiClient.SendAsync(request))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<UserFavoritesIndexViewModel>(content);
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

        public async Task RemoveAsync(int productId)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Delete, $"api/favorites"))
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
    }
}
