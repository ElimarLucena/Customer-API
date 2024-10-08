﻿using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ICustomerRepository
    {
        public Task<List<Customer>> GetAllCustomers();
        public Task<Customer> GetCustomerById(Guid customerId);
        public Task<Customer> GetCustomerByDocument(string document);
        public Task CreateCustomer(Customer customer);
        public Task UpdateCustomer(Customer customer);
        public Task DeleteCustomer(Guid customerId);
    }
}