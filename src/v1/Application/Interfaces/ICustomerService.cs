using Application.Models.CustomerModels.Request;
using Application.Models.CustomerModels.Response;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface ICustomerService
    {
        public Task<List<Customer>> GetAllCustomers();
        public Task<GetCustomerByIdResponse> GetCustomerById(int customerId);
        public Task CreateCustomer(CreateCustomerRequest customer);
        public Task UpdateCustomer(UpdateCustomerRequest customer);
        public Task DeleteCustomer(int id);
    }
}
