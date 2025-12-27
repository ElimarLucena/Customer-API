using System.Diagnostics;
using Application.Interfaces;
using Application.Models.CustomerModels.Request;
using Application.Models.CustomerModels.Response;
using Domain.Entities;
using Domain.Interfaces;
using Infra.Data.Traces;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class CustomerService(
    ILogger<CustomerService> logger,
    ICustomerRepository customerRepository
) : ICustomerService
{
    private readonly ILogger<CustomerService> _logger = logger;
    private readonly ICustomerRepository _customerRepository = customerRepository;

    public async Task<List<GetAllCustomersResponse>> GetAllCustomers()
    {
        List<GetAllCustomersResponse> response = [];

        List<Customer> allCustomers = await _customerRepository.GetAllCustomers();

        _logger.LogInformation("class: {CustomerService}, method: {GetAllCustomers}, customers found: {allCustomers.Count}.",
            nameof(CustomerService), 
            nameof(GetAllCustomers),
            allCustomers.Count);

        if (allCustomers.Any())
            foreach (Customer customer in allCustomers)
                response.Add(new GetAllCustomersResponse()
                {
                    CustomerId = customer.CustomerId,
                    Name = customer.Name,
                    Email = customer.Email,
                    Age = customer.Age,
                    Phone = customer.Phone,
                    Document = customer.Document,
                    CreatedAt = customer.CreatedAt,
                    UdatedAt = customer.UdatedAt
                });

        return response;
    }

    public async Task<GetCustomerByIdResponse> GetCustomerById(Guid customerId)
    {
        Customer customer = await _customerRepository.GetCustomerById(customerId);

        _logger.LogInformation("class: {CustomerService}, method: {GetCustomerById}, customer found: {customerId}.",
            nameof(CustomerService), 
            nameof(GetCustomerById),
            customerId);

        if (customer == null)
        {
            _logger.LogWarning("class: {CustomerService}, method: {GetCustomerById}, customer not found: {customerId}.",
                nameof(CustomerService), 
                nameof(GetCustomerById),
                customerId);

            throw new Exception("customer not found.");
        }

        GetCustomerByIdResponse response = new()
        {
            CustomerId = customer.CustomerId,
            Name = customer.Name,
            Email = customer.Email,
            Age = customer.Age,
            Phone = customer.Phone,
            Document = customer.Document,
            CreatedAt = customer.CreatedAt,
            UdatedAt = customer.UdatedAt
        };

        return response;
    }

    public async Task CreateCustomer(CreateCustomerRequest command)
    {
        using Activity? trace = Traces.ActivitySource.StartActivity("CreateCustomerService");
        trace?.SetTag("customer.name", command.Name);

        Customer existCustomer = await _customerRepository.GetCustomerByDocument(command.Document);

        if (existCustomer != null)
        {
            _logger.LogWarning("class: {CustomerService}, method: {CreateCustomer}, customer already exists: {Name}.",
                nameof(CustomerService),
                nameof(CreateCustomer),
                command.Name);

            throw new Exception("Ops! this customer already exists.");
        }

        Customer newCustomer = new()
        {
            CustomerId = Guid.NewGuid(),
            Name = command.Name,
            Email = command.Email,
            Age = command.Age,
            Phone = command.Phone,
            Document = command.Document,
            Password = command.Password,
            CreatedAt = DateTime.Now,
            UdatedAt = DateTime.Now
        };

        int result = await _customerRepository.CreateCustomer(newCustomer);

        if (result <= 0)
            _logger.LogError("class: {CustomerService}, method: {CreateCustomer}, error creating customer: {Name}.",
                nameof(CustomerService), 
                nameof(CreateCustomer),
                command.Name);
    }

    public async Task UpdateCustomer(UpdateCustomerRequest command)
    {
        Customer updateCustomer = new()
        {
            CustomerId = command.CustomerId,
            Name = command.Name,
            Email = command.Email,
            Age = command.Age,
            Phone = command.Phone,
            Document = command.Document,
            Password = command.Password,
            UdatedAt = DateTime.Now
        };

        await _customerRepository.UpdateCustomer(updateCustomer);
    }

    public async Task DeleteCustomer(Guid customerId)
    {
        await _customerRepository.DeleteCustomer(customerId);
    }
}