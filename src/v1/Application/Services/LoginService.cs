using Application.Interfaces;
using Application.Models.LoginModels.Request;
using Application.Models.LoginModels.Response;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _loginRepository;
        private readonly IAuthenticationToken _tokenAuthentication;

        public LoginService(ILoginRepository loginRepository, 
                            IAuthenticationToken tokenAuthentication)
        {
            _loginRepository = loginRepository;
            _tokenAuthentication = tokenAuthentication;
        }

        public async Task<LoginCustomerResponse> GetCustomerToken(LoginCustomerRequest request)
        {
            try
            {
                Customer customer = await _loginRepository.GetCustomerByEmailPassword(request.Email, request.Password);

                if (customer == null)
                    throw new Exception("Incorrect email address or password.");

                return new LoginCustomerResponse()
                {
                    Token = _tokenAuthentication.GenerateToken(customer)
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
