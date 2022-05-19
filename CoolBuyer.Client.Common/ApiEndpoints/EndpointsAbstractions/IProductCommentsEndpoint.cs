using CoolBuyer.Client.Common.ClientModels.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolBuyer.Client.Common.ApiEndpoints.EndpointsAbstractions
{
    public interface IProductCommentsEndpoint
    {
        Task CreateAsync(CreateProductCommentViewModel model);
        Task<IEnumerable<ProductCommentViewModel>> GetMultipleAsync(int take, int page, int productId, int? parentCommentId);
        Task UpdateAsync(UpdateProductCommentViewModel model);
        Task DeleteAsync(int productId, int commentId);
    }
}
