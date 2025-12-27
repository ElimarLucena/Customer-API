using Dapper;
using Domain.Entities;
using Domain.Interfaces;
using Infra.Data.DbContext;
using Infra.Data.Repositories.Sql;
using System.Data;
using System.Diagnostics;

namespace Infra.Data.Repositories;

public class CustomerRepository(
    ISqlServerDataBaseContext dbContext
) : ICustomerRepository
{
    private readonly ISqlServerDataBaseContext _dbContext = dbContext;

    public async Task<List<Customer>> GetAllCustomers()
    {
        using Activity? trace = Traces.Traces.ActivitySource.StartActivity("GetAllCustomersRepository");

        string query = SqlServer.GetAllCustomersQuery();

        IEnumerable<Customer> response = await _dbContext.Connection.QueryAsync<Customer>(query);

        return [.. response];
    }

    public async Task<Customer> GetCustomerById(Guid customerId)
    {
        using Activity? trace = Traces.Traces.ActivitySource.StartActivity("GetCustomerByIdRepository");
        trace?.SetTag("customer.id", customerId.ToString());

        string query = SqlServer.GetCustomerByIdQuery();

        Customer? response = await _dbContext.Connection.QuerySingleOrDefaultAsync<Customer>(query, new { CUSTOMER_ID = customerId });

        return response!;
    }

    public async Task<Customer> GetCustomerByDocument(string document)
    {
        using Activity? trace = Traces.Traces.ActivitySource.StartActivity("GetCustomerByDocumentRepository");
        trace?.SetTag("customer.document", document);

        string query = SqlServer.GetCustomerByDocumentQuery();

        Customer? response = await _dbContext.Connection.QuerySingleOrDefaultAsync<Customer>(query, new { DOCUMENT = document });

        return response!;
    }

    public async Task<int> CreateCustomer(Customer customer)
    {
        using Activity? trace = Traces.Traces.ActivitySource.StartActivity("InsertCustomerRepository");
        trace?.SetTag("customer.id", customer.CustomerId.ToString());

        DynamicParameters parameters = new();

        parameters.Add("CUSTOMER_ID", customer.CustomerId, DbType.Guid);
        parameters.Add("NAME", customer.Name, DbType.String);
        parameters.Add("EMAIL", customer.Email, DbType.String);
        parameters.Add("DOCUMENT", customer.Document, DbType.String);
        parameters.Add("PHONE", customer.Phone, DbType.Int64);
        parameters.Add("AGE", customer.Age, DbType.Int32);
        parameters.Add("PASSWORD", customer.Password, DbType.String);
        parameters.Add("CREATED_AT", customer.CreatedAt, DbType.DateTime);
        parameters.Add("UPDATED_AT", customer.UdatedAt, DbType.DateTime);

        string command = SqlServer.CreateCustomerCommand();

        int response = await _dbContext.Connection.ExecuteAsync(sql: command, param: parameters, commandTimeout: 60);

        return response;
    }

    public async Task<int> UpdateCustomer(Customer customer)
    {
        using Activity? trace = Traces.Traces.ActivitySource.StartActivity("UpdateCustomerRepository");
        trace?.SetTag("customer.id", customer.CustomerId.ToString());

        DynamicParameters parameters = new();

        parameters.Add("CUSTOMER_ID", customer.CustomerId, DbType.Guid);
        parameters.Add("NAME", customer.Name, DbType.String);
        parameters.Add("EMAIL", customer.Email, DbType.String);
        parameters.Add("DOCUMENT", customer.Document, DbType.String);
        parameters.Add("PHONE", customer.Phone, DbType.Int64);
        parameters.Add("AGE", customer.Age, DbType.Int32);
        parameters.Add("PASSWORD", customer.Password, DbType.String);
        parameters.Add("UPDATED_AT", customer.UdatedAt, DbType.DateTime);

        string command = SqlServer.UpdateCustomerCommand();

        int response = await _dbContext.Connection.ExecuteAsync(sql: command, param: parameters, commandTimeout: 60);

        return response;
    }

    public async Task<int> DeleteCustomer(Guid customerId)
    {
        using Activity? trace = Traces.Traces.ActivitySource.StartActivity("DeleteCustomerRepository");
        trace?.SetTag("customer.id", customerId.ToString());

        string command = SqlServer.DeleteCustomerCommand();

        DynamicParameters parameters = new();

        parameters.Add("CUSTOMER_ID", customerId, DbType.Guid);

        int response = await _dbContext.Connection.ExecuteAsync(sql: command, param: parameters, commandTimeout: 60);

        return response;
    }
}
