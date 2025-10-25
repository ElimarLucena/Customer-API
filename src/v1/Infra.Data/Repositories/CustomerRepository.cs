using Dapper;
using Domain.Entities;
using Domain.Interfaces;
using Infra.Data.DbContext;
using Infra.Data.Repositories.Sql;
using System.Data;

namespace Infra.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ISqlServerDataBaseContext _dbContext;

        public CustomerRepository(ISqlServerDataBaseContext dbContext) => _dbContext = dbContext;

        public async Task<List<Customer>> GetAllCustomers()
        {
            string query = SqlServer.GetAllCustomersQuery();

            IEnumerable<Customer> response = await _dbContext.Connection.QueryAsync<Customer>(query);

            return response.ToList();
        }

        public async Task<Customer> GetCustomerById(Guid customerId)
        {
            string query = SqlServer.GetCustomerByIdQuery();

            Customer? response = await _dbContext.Connection.QuerySingleOrDefaultAsync<Customer>(query, new { CustomerId = customerId });

            return response!;
        }

        public async Task<Customer> GetCustomerByDocument(string document)
        {
            string query = SqlServer.GetCustomerByDocumentQuery();

            Customer? response = await _dbContext.Connection.QuerySingleOrDefaultAsync<Customer>(query, new { Document = document });

            return response!;
        }

        public async Task<int> CreateCustomer(Customer customer)
        {
            DynamicParameters parameters = new();

            parameters.Add("customerId", customer.CustomerId, DbType.Guid);
            parameters.Add("name", customer.Name, DbType.String);
            parameters.Add("email", customer.Email, DbType.String);
            parameters.Add("document", customer.Document, DbType.String);
            parameters.Add("phone", customer.Phone, DbType.Int64);
            parameters.Add("age", customer.Age, DbType.Int32);
            parameters.Add("password", customer.Password, DbType.String);

            string command = SqlServer.CreateCustomerCommand();

            int response = await _dbContext.Connection.ExecuteAsync(sql: command, param: parameters, commandTimeout: 60);

            return response;
        }

        public async Task<int> UpdateCustomer(Customer customer)
        {
            DynamicParameters parameters = new();

            parameters.Add("customerId", customer.CustomerId, DbType.Guid);
            parameters.Add("name", customer.Name, DbType.String);
            parameters.Add("email", customer.Email, DbType.String);
            parameters.Add("document", customer.Document, DbType.String);
            parameters.Add("phone", customer.Phone, DbType.Int64);
            parameters.Add("age", customer.Age, DbType.Int32);
            parameters.Add("password", customer.Password, DbType.String);

            string command = SqlServer.UpdateCustomerCommand();

            int response = await _dbContext.Connection.ExecuteAsync(sql: command, param: parameters, commandTimeout: 60);

            return response;
        }

        public async Task<int> DeleteCustomer(Guid customerId)
        {
            string command = SqlServer.DeleteCustomerCommand();

            DynamicParameters parameters = new();

            parameters.Add("customerId", customerId, DbType.Guid);

            int response = await _dbContext.Connection.ExecuteAsync(sql: command, param: parameters, commandTimeout: 60);

            return response;
        }
    }
}
