using Application.Models.LoginModels.Request;
using Application.Models.LoginModels.Response;

namespace Application.Interfaces
{
    public interface ILoginService
    {
        public Task<LoginCustomerResponse> GetCustomerToken(LoginCustomerRequest request);
    }
}