using Dapper;
using Domain.Entities;
using Domain.Interfaces;
using Infra.Data.DbContext;

namespace Infra.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ISqlServerDataBaseContext _dbContext;

        public CustomerRepository(ISqlServerDataBaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Customer>> GetAllCustomers()
        {
            string query = 
                @"SELECT 
                        [CustomerId],
                        [Name],
                        [Email],
                        [Age],
                        [Phone],
                        [Document]
                   FROM 
                        [Customer].[dbo].[TB_CUSTOMERS]";

            var response = await _dbContext.Connection.QueryAsync<Customer>(query);

            return response.ToList();
        }

        public async Task<Customer> GetCustomerById(int customerId)
        {
            string query =
                @"SELECT 
                        [CustomerId],
                        [Name],
                        [Email],
                        [Age],
                        [Phone],
                        [Document]
                  FROM  
                        [Customer].[dbo].[TB_CUSTOMERS] 
                  WHERE 
                        CustomerId = @customerId";

            var response = await _dbContext.Connection.QuerySingleOrDefaultAsync<Customer>(query, new { CustomerId = customerId });

            return response;
        }

        public async Task<Customer> GetCustomerByDocument(string document)
        {
            string query =
                @"SELECT 
                        [CustomerId],
                        [Name],
                        [Email],
                        [Age],
                        [Phone],
                        [Document]
                  FROM 
                        [Customer].[dbo].[TB_CUSTOMERS] 
                  WHERE 
                        Document = @document";

            var response = await _dbContext.Connection.QuerySingleOrDefaultAsync<Customer>(query, new { Document = document });

            return response;
        }

        public async Task CreateCustomer(Customer customer)
        {
            var parameters = new DynamicParameters();
            parameters.Add("name", customer.Name, System.Data.DbType.String);
            parameters.Add("email", customer.Email, System.Data.DbType.String);
            parameters.Add("document", customer.Document, System.Data.DbType.String);
            parameters.Add("phone", customer.Phone, System.Data.DbType.Int64);
            parameters.Add("age", customer.Age, System.Data.DbType.Int32);

            string command =
                @"INSERT INTO 
                        [Customer].[dbo].[TB_CUSTOMERS](Name, Email, Document, Phone, Age) 
                  VALUES
                        (@name, @email, @document, @phone, @age)";

            await _dbContext.Connection.ExecuteAsync(command, parameters);
        }

        public async Task UpdateCustomer(Customer customer)
        {
            var parameters = new DynamicParameters();
            parameters.Add("CustomerId", customer.CustomerId, System.Data.DbType.Int32);
            parameters.Add("name", customer.Name, System.Data.DbType.String);
            parameters.Add("email", customer.Email, System.Data.DbType.String);
            parameters.Add("document", customer.Document, System.Data.DbType.String);
            parameters.Add("phone", customer.Phone, System.Data.DbType.Int64);
            parameters.Add("age", customer.Age, System.Data.DbType.Int32);

            string command =
                @"UPDATE
                        [Customer].[dbo].[TB_CUSTOMERS]
                  SET
                        Name = @name,
                        Email = @email,
                        Document = @document,
                        Phone = @phone,
                        Age =  @age
                  WHERE 
                        CustomerId = @CustomerId";

            await _dbContext.Connection.ExecuteAsync(command, parameters);
        }

        public async Task DeleteCustomer(int customerId)
        {
            string command =
                @"DELETE FROM 
                        [Customer].[dbo].[TB_CUSTOMERS]
                  WHERE
                        CustomerId = @CustomerId";

            await _dbContext.Connection.ExecuteAsync(command, new { CustomerId = customerId });
        }
    }
}
