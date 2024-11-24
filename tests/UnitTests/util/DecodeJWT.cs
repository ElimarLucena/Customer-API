using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using UnitTests.util.Models.JwtModels;

namespace UnitTests.util
{
    public static class DecodeJWT
    {
        public static JwtToken JwtToken(string token)
        {
            JwtSecurityToken jwtSecurityToken = new JwtSecurityTokenHandler().ReadJwtToken(token);

            JwtToken response = new()
            {
                SigningAlgorithm = jwtSecurityToken.SignatureAlgorithm,
                TokenType = jwtSecurityToken.Header.Typ
            };

            foreach (Claim claim in jwtSecurityToken.Claims) 
            {
                switch(claim.Type)
                {
                    case "nameid":
                        response.CustomerId = Guid.Parse(claim.Value);
                        break;
                    case "unique_name":
                        response.Name = claim.Value;
                        break;
                    case "exp":
                        DateTimeOffset exp = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(claim.Value));
                        response.Expiration = exp.DateTime;
                        break;
                    default:
                        break;
                }
            }

            return response;
        }
    }
}
