using CoolBuyer.Server.BusinessLogic.Services;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CoolBuyer.Server.Authentication.Services
{
    /// <summary>
    /// Generates JWT encoded token.
    /// </summary>
    public class JwtGeneratorService : ITokenGeneratorService
    {
        byte[] secretKey;
        JwtSecurityTokenHandler tokenHandler;
        

        public JwtGeneratorService()
        {
            tokenHandler = new JwtSecurityTokenHandler();
            // inject key!!!! (web.config)
            secretKey = Encoding.UTF8.GetBytes("apcogl4iof84A0Vi23of029va8XLd92l");
        }
        

        public string GenerateToken(Claim[] claims)
        {
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                // get expiration from web.config
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(secretKey),
                    SecurityAlgorithms.HmacSha256Signature
                ),
                IssuedAt = DateTime.UtcNow
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtString = tokenHandler.WriteToken(token);

            return jwtString;
        }

        public ClaimsPrincipal VerifyToken(string token)
        {
            if (token != null)
            {
                token = token.Replace("\"", "");
            }
            
            SecurityToken validatedToken;
            ClaimsPrincipal claims = new ClaimsPrincipal();
            try
            {
                claims = tokenHandler.ValidateToken(token, new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                    ValidateLifetime = true,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ClockSkew = TimeSpan.Zero
                }, out validatedToken);

            }
            catch (Exception ex)
            {
                // do nothing
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            return claims;
        }
    }
}
