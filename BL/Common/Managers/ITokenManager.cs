using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CoolBuyer.Server.BusinessLogic.Common.Managers
{
    public interface ITokenManager
    {
        string GenerateToken(Claim[] claims);
        ClaimsPrincipal VerifyToken(string token);
    }
}
