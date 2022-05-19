using CoolBuyer.Server.BusinessLogic.Common.DTOs.Request.Write;
using CoolBuyer.Server.BusinessLogic.Common.DTOs.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolBuyer.Server.BusinessLogic.Services
{
    public interface IFavoritesService
    {
        void Add(AddProductToFavoritesDTO model);
        void Delete(int productId);
        IEnumerable<UserFavoriteDTO> Get(int take = 5, int page = 1);
    }
}
