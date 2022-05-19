using CoolBuyer.Client.Common.ClientModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolBuyer.Client.Common.ApiEndpoints.EndpointsAbstractions
{
    public interface IAccountEndpoint
    {
        Task RegisterAsync(RegistrationDetailsViewModel model);
        Task ConfirmAccountAsync(string token, int userId);
        Task<AuthenticatedUserViewModel> LoginAsync(UserLoginViewModel model);
    }
}
