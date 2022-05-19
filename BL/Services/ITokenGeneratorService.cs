using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CoolBuyer.Server.BusinessLogic.Services
{
    public interface ITokenGeneratorService
    {
        string GenerateToken(Claim[] claims);
        ClaimsPrincipal VerifyToken(string token);
    }
}
