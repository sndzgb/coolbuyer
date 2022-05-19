using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace CoolBuyer.Server.WebApi.Security.Principal
{
    public interface ICoolBuyerPrincipal : IPrincipal
    {
        string Email { get; }
        int Id { get; }
        string Username { get; }
    }
}
