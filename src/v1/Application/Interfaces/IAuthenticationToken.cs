using Domain.Entities;

namespace Application.Interfaces
{
    public interface IAuthenticationToken
    {
        public string GenerateToken(Customer customer);
    }
}
