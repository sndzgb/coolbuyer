using CoolBuyer.Server.BusinessLogic.Common.DTOs;
using CoolBuyer.Server.BusinessLogic.Common.DTOs.Request.Read;
using CoolBuyer.Server.BusinessLogic.Common.DTOs.Request.Write;
using CoolBuyer.Server.BusinessLogic.Common.DTOs.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolBuyer.Server.BusinessLogic.Services
{
    public interface IProductService
    {
        void Create(NewProductDTO product);
        void Update(UpdateProductDTO updatedProduct);
        void Delete(int productId);

        void LikeDislike(LikeDislikeProductDTO model);
        IndexPageProductDetailsDTO GetIndexPageProducts();
        
        ProductSearchDTO SearchProducts(ProductsSearchOptionsDTO searchOptions);
        ProductDetailsDTO GetSingleProductDetails(int productId);
    }
}
