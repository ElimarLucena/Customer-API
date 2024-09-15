using Dapper;
using Domain.Entities;
using Domain.Interfaces;
using Infra.Data.DbContext;
using Infra.Data.Repositories.Sql;

namespace Infra.Data.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        private readonly ISqlServerDataBaseContext _dbContext;

        public LoginRepository(ISqlServerDataBaseContext dbContext) => _dbContext = dbContext;

        public async Task<Customer> GetCustomerByEmailPassword(string email, string password)
        {
            string query = SqlServer.GetCustomerByEmailPassword_Query();

            Customer? response = await _dbContext.Connection.QuerySingleOrDefaultAsync<Customer>(query, new { Email = email, Password = password });

            return response!;
        }
    }
}
