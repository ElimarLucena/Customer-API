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
        private readonly IAuthenticationToken _authenticationToken;

        public LoginService(ILoginRepository loginRepository, 
                            IAuthenticationToken authenticationToken)
        {
            _loginRepository = loginRepository;
            _authenticationToken = authenticationToken;
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
                    Token = _authenticationToken.GenerateToken(customer)
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
