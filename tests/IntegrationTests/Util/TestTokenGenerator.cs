using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace IntegrationTests.util
{
    public static class TestTokenGenerator
    {
        private static string GenerateToken()
        {
            JwtSecurityTokenHandler tokenHandler = new();

            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new(ClaimTypes.NameIdentifier, "3b848ecb-8611-409c-b741-634f8f053ba6"),
                    new(ClaimTypes.Name, "TestCustomer"),
                    new(ClaimTypes.Role, "Admin")
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials
                (
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes("JWTAuthenticationSecuredTest7630b55d7c954cf293283686889427ec")),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public static string GetToken()
            => GenerateToken();
    }
}