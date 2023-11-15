using Application.Interfaces;
using Application.Models.LoginModels.Request;
using Application.Models.LoginModels.Response;
using MediatR;

namespace Application.UseCases.Handlers
{
    public class LoginHandler : IRequestHandler<LoginCustomerRequest, LoginCustomerResponse>
    {
        private readonly ILoginService _loginService;

        public LoginHandler(ILoginService loginService) => _loginService = loginService;

        public async Task<LoginCustomerResponse> Handle(LoginCustomerRequest request, CancellationToken cancellationToken)
        {
            LoginCustomerResponse response = await _loginService.GetCustomerToken(request);

            return response;
        }
    }
}