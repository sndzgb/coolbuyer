using CoolBuyer.Client.Common.ClientModels.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolBuyer.Client.Common.ApiEndpoints.EndpointsAbstractions
{
    public interface IProductsEndpoint
    {
        Task CreateAsync(NewProductViewModel model);
        Task DeleteAsync(int productId);
        Task UpdateAsync(int id, UpdateProductDetailsViewModel model);

        Task<IndexPageProductDetailsViewModel> GetIndexPageProductsAsync();
        Task LikeDislikeAsync(LikeDislikeProductViewModel model);

        Task<ProductDetailsViewModel> GetSingleAsync(int productId);
        Task<ProductsSearchViewModel> SearchAsync(ProductsSearchOptionsViewModel model);
    }
}
