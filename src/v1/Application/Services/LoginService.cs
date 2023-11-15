using Application.Interfaces;
using Application.Models.LoginModels.Request;
using Application.Models.LoginModels.Response;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Services
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _loginRepository;

        public LoginService(ILoginRepository loginRepository) => _loginRepository = loginRepository;

        public async Task<LoginCustomerResponse> GetCustomerToken(LoginCustomerRequest request)
        {
            try
            {
                Customer customer = await _loginRepository.GetCustomerByEmailPassword(request.Email, request.Password);

                if (customer == null)
                    throw new Exception("Ops! E-mail or Password not found.");

                return new LoginCustomerResponse()
                {
                    Token = GenerateToken(customer)
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static string GenerateToken(Customer customer)
        {
            JwtSecurityTokenHandler tokenHandler = new();

            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, customer.CustomerId.ToString()),
                    new Claim(ClaimTypes.Name, customer.Name.ToString()),
                    new Claim(ClaimTypes.Email, customer.Email.ToString()),
                    new Claim(ClaimTypes.MobilePhone, customer.Phone.ToString()),
                }),
                Expires = DateTime.Now.AddHours(1),
                SigningCredentials = new SigningCredentials
                (
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes("JWTAuthenticationSecured7630b55d7c954cf493283886888427ec")),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
