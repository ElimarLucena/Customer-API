﻿using Application.Models.CustomerModels.Request;
using Application.Models.CustomerModels.Response;

namespace Application.Interfaces
{
    public interface ICustomerService
    {
        public Task<List<GetAllCustomersResponse>> GetAllCustomers();
        public Task<GetCustomerByIdResponse> GetCustomerById(Guid customerId);
        public Task CreateCustomer(CreateCustomerRequest customer);
        public Task UpdateCustomer(UpdateCustomerRequest customer);
        public Task DeleteCustomer(Guid customerId);
    }
}
