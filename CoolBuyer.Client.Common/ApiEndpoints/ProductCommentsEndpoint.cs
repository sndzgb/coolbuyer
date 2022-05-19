using CoolBuyer.Client.Common.ApiEndpoints.EndpointsAbstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoolBuyer.Client.Common.ClientModels.Comments;
using CoolBuyer.Client.Common.ApiClient;
using System.Net.Http;
using Newtonsoft.Json;
using CoolBuyer.Client.Common.ClientModels.Errors;
using CoolBuyer.Client.Common.ClientExceptions;

namespace CoolBuyer.Client.Common.ApiEndpoints
{
    public class ProductCommentsEndpoint : IProductCommentsEndpoint
    {
        private readonly ICoolBuyerHttpClient httpClient;


        public ProductCommentsEndpoint(ICoolBuyerHttpClient httpClient)
        {
            this.httpClient = httpClient;
        }


        public async Task CreateAsync(CreateProductCommentViewModel model)
        {
            var uri = $"/api/products/{model.ProductId}/comments";
            using (var request = new HttpRequestMessage(HttpMethod.Post, uri))
            {
                var stringContent = JsonConvert.SerializeObject(model);
                using (var content = new StringContent(stringContent))
                {
                    request.Content = content;
                    using (var response = await httpClient.ApiClient.SendAsync(request))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            return;
                        }
                        else
                        {
                            var responseContent = await response.Content.ReadAsStringAsync();
                            var errorResponse = JsonConvert.DeserializeObject<WebApiErrorResponseModel>(responseContent);
                            throw new ApiCallException(errorResponse.ErrorMessage, errorResponse.StatusCode, errorResponse.Errors);
                        }
                    }
                }
            }
        }

        public async Task DeleteAsync(int productId, int commentId)
        {
            var uri = $"/api/products/{productId}/comments/{commentId}";
            using (var request = new HttpRequestMessage(HttpMethod.Delete, uri))
            {
                using (var response = await httpClient.ApiClient.SendAsync(request))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return;
                    }
                    else
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        var errorResponse = JsonConvert.DeserializeObject<WebApiErrorResponseModel>(responseContent);
                        throw new ApiCallException(errorResponse.ErrorMessage, errorResponse.StatusCode, errorResponse.Errors);
                    }
                }
            }
        }

        public async Task<IEnumerable<ProductCommentViewModel>> GetMultipleAsync(int take, int page, int productId, int? parentCommentId)
        {
            var uri = $"/api/products/{productId}/comments?take={take}&page={page}&productId={productId}{(parentCommentId != null ? $"&parentCommentId={parentCommentId}" : "")}";
            using (var request = new HttpRequestMessage(HttpMethod.Get, uri))
            {
                using (var response = await httpClient.ApiClient.SendAsync(request))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        var responseContent = JsonConvert.DeserializeObject<IEnumerable<ProductCommentViewModel>>(json);
                        return responseContent;
                    }
                    else
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        var errorResponse = JsonConvert.DeserializeObject<WebApiErrorResponseModel>(responseContent);
                        throw new ApiCallException(errorResponse.ErrorMessage, errorResponse.StatusCode, errorResponse.Errors);
                    }
                }
            }
        }

        public async Task UpdateAsync(UpdateProductCommentViewModel model)
        {
            var uri = $"/api/products/{model.ProductId}/comments/{model.CommentId}";
            using (var request = new HttpRequestMessage(HttpMethod.Put, uri))
            {
                var stringContent = JsonConvert.SerializeObject(model);
                using (var content = new StringContent(stringContent))
                {
                    request.Content = content;
                    using (var response = await httpClient.ApiClient.SendAsync(request))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            return;
                        }
                        else
                        {
                            var responseContent = await response.Content.ReadAsStringAsync();
                            var errorResponse = JsonConvert.DeserializeObject<WebApiErrorResponseModel>(responseContent);
                            throw new ApiCallException(errorResponse.ErrorMessage, errorResponse.StatusCode, errorResponse.Errors);
                        }
                    }
                }
            }
        }
    }
}
