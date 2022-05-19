using CoolBuyer.Client.Common.ClientModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolBuyer.Client.Common.ApiEndpoints.EndpointsAbstractions
{
    public interface IUsersEndpoint
    {
        Task<UserProfileIndexDetailsViewModel> GetSingleAsync(int userId, int take = 5, int page = 1);
    }
}
