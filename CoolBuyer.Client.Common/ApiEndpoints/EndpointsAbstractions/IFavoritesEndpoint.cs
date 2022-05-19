using CoolBuyer.Client.Common.ClientModels.Favorites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolBuyer.Client.Common.ApiEndpoints.EndpointsAbstractions
{
    public interface IFavoritesEndpoint
    {
        Task AddOrRemoveAsync(AddOrRemoveFavoriteViewModel model);
        Task<UserFavoritesIndexViewModel> GetAsync(int take, int page);
    }
}
