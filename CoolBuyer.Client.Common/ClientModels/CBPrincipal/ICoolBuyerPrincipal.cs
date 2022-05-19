using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace CoolBuyer.Client.Common.ClientModels.CBPrincipal
{
    public interface ICoolBuyerPrincipal : IPrincipal
    {
        int Id { get; }
        string Username { get; }
        //DateTime TokenExpirationDateUTC { get; }
        // SessionExpirationDateUTC
        string EncodedToken { get; }
    }
}
