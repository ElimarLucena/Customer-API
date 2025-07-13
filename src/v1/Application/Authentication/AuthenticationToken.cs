using Application.Interfaces;
using Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Authentication
{
    public class AuthenticationToken : IAuthenticationToken
    {
        private readonly string _key;

        public AuthenticationToken(string key) => _key = key;

        public string GenerateToken(Customer customer)
        {
            JwtSecurityTokenHandler tokenHandler = new();

            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new(ClaimTypes.NameIdentifier, customer.CustomerId.ToString()),
                    new(ClaimTypes.Name, customer.Name.ToString())
                }),
                NotBefore = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials
                (
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_key)),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
