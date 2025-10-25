using Application.Interfaces;
using Application.Models.CustomerModels.Request;
using Application.Models.CustomerModels.Response;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository) => _customerRepository = customerRepository;

        public async Task<List<GetAllCustomersResponse>> GetAllCustomers()
        {
            List<GetAllCustomersResponse> response = new();

            List<Customer> allCustomers = await _customerRepository.GetAllCustomers();

            if (allCustomers.Any())
                foreach (Customer customer in allCustomers)
                    response.Add(new GetAllCustomersResponse() 
                                {
                                    CustomerId = customer.CustomerId,
                                    Name = customer.Name,
                                    Email = customer.Email,
                                    Age = customer.Age,
                                    Phone = customer.Phone,
                                    Document = customer.Document
                                });

            return response;
        }

        public async Task<GetCustomerByIdResponse> GetCustomerById(Guid customerId) 
        { 
            Customer customer = await _customerRepository.GetCustomerById(customerId);

            if (customer == null) 
                throw new Exception("Ops! this customer not found.");

            GetCustomerByIdResponse response = new()
            {
                CustomerId = customer.CustomerId,
                Name = customer.Name,
                Email = customer.Email,
                Age = customer.Age,
                Phone = customer.Phone,
                Document = customer.Document
            };

            return response;
        }

        public async Task CreateCustomer(CreateCustomerRequest command)
        {
            Customer existCustomer = await _customerRepository.GetCustomerByDocument(command.Document);

            if (existCustomer != null)
                throw new Exception("Ops! this customer already exists.");

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

            await _customerRepository.CreateCustomer(newCustomer);
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
}
