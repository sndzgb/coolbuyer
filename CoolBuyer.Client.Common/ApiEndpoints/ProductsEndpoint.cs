using CoolBuyer.Client.Common.ApiEndpoints.EndpointsAbstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoolBuyer.Client.Common.ClientModels.Products;
using CoolBuyer.Client.Common.ApiClient;
using System.Net.Http;
using System.IO;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using CoolBuyer.Client.Common.Services;
using CoolBuyer.Client.Common.ClientExceptions;
using CoolBuyer.Client.Common.ClientModels.Errors;
using CoolBuyer.Client.Common.ClientModels.Pagination;

namespace CoolBuyer.Client.Common.ApiEndpoints
{
    public class ProductsEndpoint : IProductsEndpoint
    {
        private readonly ICoolBuyerHttpClient httpClient;
        private readonly IAuthenticatedUserService authenticatedUserService;


        public ProductsEndpoint(ICoolBuyerHttpClient httpClient, IAuthenticatedUserService authenticatedUserService)
        {
            this.httpClient = httpClient;
            this.authenticatedUserService = authenticatedUserService;
        }


        public async Task CreateAsync(NewProductViewModel model)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Post, "/api/products"))
            {
                using (var content = new MultipartFormDataContent())
                {
                    var stringContent = new StringContent(JsonConvert.SerializeObject(model));
                    stringContent.Headers.Add("Content-Disposition", "form-data; name=\"json\"");
                    content.Add(stringContent);

                    foreach (var i in model.Images)
                    {
                        int c = 0;
                        var streamContent = new StreamContent(i.Stream);
                        streamContent.Headers
                            .Add("Content-Type", "application/octet-stream");
                        streamContent.Headers
                            .Add("Content-Disposition", $"form-data; name=\"{string.Concat("file", c)}\"; filename=\"" + Path.GetFileName(i.Name) + "\"");
                        content
                            .Add(streamContent, $"{string.Concat("file", c)}", Path.GetFileName(i.Name));
                        c++;
                    }
                    
                    request.Headers.Authorization = new AuthenticationHeaderValue("bearer", authenticatedUserService.Token);
                    request.Content = content;

                    using (var response = await httpClient.ApiClient.SendAsync(request))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            return;
                        }
                        else
                        {
                            var errorResponse = JsonConvert.DeserializeObject<WebApiErrorResponseModel>(await response.Content.ReadAsStringAsync());
                            throw new ApiCallException(errorResponse.ErrorMessage, errorResponse.StatusCode, errorResponse.Errors);
                        }
                    }
                }
            }
        }

        public async Task DeleteAsync(int productId)
        {
            var uri = $"/api/products/{productId}";

            using (var request = new HttpRequestMessage(HttpMethod.Delete, uri))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("bearer", authenticatedUserService.Token);

                using (var response = await httpClient.ApiClient.SendAsync(request))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return;
                    }
                    else
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var errorResponse = JsonConvert.DeserializeObject<WebApiErrorResponseModel>(content);
                        throw new ApiCallException(errorResponse.ErrorMessage, errorResponse.StatusCode, errorResponse.Errors);
                    }
                }
            }
        }

        public async Task<ProductDetailsViewModel> GetSingleAsync(int productId)
        {
            var uri = $"/api/products/{productId}";

            using (var request = new HttpRequestMessage(HttpMethod.Get, uri))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("bearer", authenticatedUserService.Token);
                using (var response = await httpClient.ApiClient.SendAsync(request))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        var product = JsonConvert.DeserializeObject<ProductDetailsViewModel>(json);
                        return product;
                    }
                    else
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var errorResponse = JsonConvert.DeserializeObject<WebApiErrorResponseModel>(content);
                        throw new ApiCallException(errorResponse.ErrorMessage, errorResponse.StatusCode, errorResponse.Errors);
                    }
                }
            }
        }

        public async Task<ProductsSearchViewModel> SearchAsync(ProductsSearchOptionsViewModel model)
        {
            var baseAddress = httpClient.ApiClient.BaseAddress;
            string query = string.Concat(                    
                    baseAddress,
                    "api/products",
                    "?",
                    "searchString=", model.SearchString, "&",
                    "maxPrice=", model.MaxPrice, "&",
                    "minPrice=", model.MinPrice, "&",
                    "categoryId=", model.CategoryId, "&",
                    "subcategoryId=", model.SubcategoryId, "&",
                    "sectionId=", model.SectionId, "&",
                    "page=", model.Page, "&",
                    "take=", model.Take, "&",
                    "sortBy=", model.SortBy
                );
            using (var request = new HttpRequestMessage(HttpMethod.Get, query))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("bearer", authenticatedUserService.Token);
                
                using (var response = await httpClient.ApiClient.SendAsync(request))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        var products = JsonConvert.DeserializeObject<ProductsSearchViewModel>(json);
                        products.SearchOptions = model;
                        products.Pagination = new PagerViewModel(products.Stats.TotalItemsFound, model.Page, model.Take);
                        return products;
                    }
                    else
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var errorResponse = JsonConvert.DeserializeObject<WebApiErrorResponseModel>(content);
                        throw new ApiCallException(errorResponse.ErrorMessage, errorResponse.StatusCode, errorResponse.Errors);
                    }
                }
            }
        }

        public async Task UpdateAsync(int id, UpdateProductDetailsViewModel model)
        {
            var uri = $"/api/products/{id}";

            using (var request = new HttpRequestMessage(HttpMethod.Put, uri))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("bearer", authenticatedUserService.Token);

                using (var multipartContent = new MultipartFormDataContent())
                {
                    var stringContent = new StringContent(JsonConvert.SerializeObject(model));
                    stringContent.Headers.Add("Content-Disposition", "form-data; name=\"json\"");
                    multipartContent.Add(stringContent);

                    foreach (var i in model.Images)
                    {
                        int counter = 0;
                        var streamContent = new StreamContent(i.Stream);
                        streamContent.Headers
                            .Add("Content-Type", "application/octet-stream");
                        streamContent.Headers
                            .Add("Content-Disposition", $"form-data; name=\"{string.Concat("file", counter)}\"; filename=\"" + Path.GetFileName(i.Name) + "\"");
                        multipartContent
                            .Add(streamContent, $"{string.Concat("file", counter)}", Path.GetFileName(i.Name));
                        counter++;
                    }

                    request.Headers.Authorization = new AuthenticationHeaderValue("bearer", authenticatedUserService.Token);
                    request.Content = multipartContent;

                    using (var response = await httpClient.ApiClient.SendAsync(request))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            return;
                        }
                        else
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            var errorResponse = JsonConvert.DeserializeObject<WebApiErrorResponseModel>(content);
                            throw new ApiCallException(errorResponse.ErrorMessage, errorResponse.StatusCode, errorResponse.Errors);
                        }
                    }
                }
            }
        }

        public async Task LikeDislikeAsync(LikeDislikeProductViewModel model)
        {
            var uri = $"/api/products/{model.ProductId}/react";

            using (var request = new HttpRequestMessage(HttpMethod.Post, uri))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("bearer", authenticatedUserService.Token);

                var stringContent = new StringContent(JsonConvert.SerializeObject(model));
                request.Content = stringContent;

                using (var response = await httpClient.ApiClient.SendAsync(request))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return;
                    }
                    else
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var errorResponse = JsonConvert.DeserializeObject<WebApiErrorResponseModel>(content);
                        throw new ApiCallException(errorResponse.ErrorMessage, errorResponse.StatusCode, errorResponse.Errors);
                    }
                }
            }
        }

        public async Task<IndexPageProductDetailsViewModel> GetIndexPageProductsAsync()
        {
            var uri = "/api/products/index";

            using (var request = new HttpRequestMessage(HttpMethod.Get, uri))
            {
                using (var response = await httpClient.ApiClient.SendAsync(request))
                {
                    var content = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        var products = JsonConvert.DeserializeObject<IndexPageProductDetailsViewModel>(content);
                        return products;
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
