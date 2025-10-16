using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConsultorioFunctions
{
    public static class TokenValidation
    {
        public static bool UnauthorizedResponse(HttpRequestData req, out string message)
        {
            message = string.Empty;

            if (!req.Headers.TryGetValues("Authorization", out var authHeader))
            {
                message = "Missing Authorization header.";
                return false;
            }
            if (!TokenValidation.ValidateToken(authHeader.FirstOrDefault()!))
            {
                message = "Invalid or expired token.";
                return false;
            }

            return true;
        }

        public static bool ValidateToken(string tokenReq)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = Environment.GetEnvironmentVariable("ISSUER"),
                ValidAudience = Environment.GetEnvironmentVariable("AUDIENCE"),
                IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("KEY")!))
            };

            string token = tokenReq.Replace("Bearer ", string.Empty);

            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);
                // Token válido - acesse as claims via principal.Claims
            }
            catch (SecurityTokenException)
            {
                return false;
            }

            return true;
        }
    }
}
