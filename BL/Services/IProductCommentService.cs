using CoolBuyer.Server.BusinessLogic.Common.DTOs.Read;
using CoolBuyer.Server.BusinessLogic.Common.DTOs.Write;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolBuyer.Server.BusinessLogic.Services
{
    public interface IProductCommentService
    {
        ProductCommentDetailsDTO GetSingle(int productId, int commentId);
        IEnumerable<ProductCommentDetailsDTO> GetMultiple(FilterProductCommentsDTO model);
        
        void Create(NewProductCommentDTO newComment, int userId);
        void Delete(int commentId, int userId);
        void EditUpdate(UpdateProductCommentDTO updatedComment, int userId);
        void LikeDislikeComment(ProductCommentReactionDTO model, int userId);
        void FlagAsInappropriate(int commentId, int userId);
    }
}
