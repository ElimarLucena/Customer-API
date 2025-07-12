using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ICustomerRepository
    {
        public Task<List<Customer>> GetAllCustomers();
        public Task<Customer> GetCustomerById(Guid customerId);
        public Task<Customer> GetCustomerByDocument(string document);
        public Task<int> CreateCustomer(Customer customer);
        public Task<int> UpdateCustomer(Customer customer);
        public Task<int> DeleteCustomer(Guid customerId);
    }
}