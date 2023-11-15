using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ILoginRepository
    {
        public Task<Customer> GetCustomerByEmailPassword(string email, string password);
    }
}
