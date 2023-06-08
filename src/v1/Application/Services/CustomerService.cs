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

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<List<Customer>> GetAllCustomers()
        {
            try
            {
                List<Customer> response = await _customerRepository.GetAllCustomers();

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<GetCustomerByIdResponse> GetCustomerById(int customerId) 
        { 
            try
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
                    Document = customer.Document,
                };

                return response;
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task CreateCustomer(CreateCustomerRequest command)
        {
            try
            {
                Customer existCustomer = await _customerRepository.GetCustomerByDocument(command.Document);

                if (existCustomer != null)
                    throw new Exception("Ops! this customer already exists.");

                Customer newCustomer = new()
                {
                    Name = command.Name,
                    Email = command.Email,
                    Age = command.Age,
                    Phone = command.Phone,
                    Document = command.Document
                };

                await _customerRepository.CreateCustomer(newCustomer);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateCustomer(UpdateCustomerRequest command)
        {
            try
            {
                Customer updateCustomer = new()
                {
                    CustomerId = command.CustomerId,
                    Name = command.Name,
                    Email = command.Email,
                    Age = command.Age,
                    Phone = command.Phone,
                    Document = command.Document
                };

                await _customerRepository.UpdateCustomer(updateCustomer);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteCustomer(int id)
        {
            try
            {
                await _customerRepository.DeleteCustomer(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
