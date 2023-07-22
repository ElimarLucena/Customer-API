﻿using MediatR;
using Application.Interfaces;
using Application.Models.CustomerModels.Request;
using Application.Models.CustomerModels.Response;
using Domain.Entities;

namespace Application.UseCases.Handlers
{
    public class CustomerHandler :
        IRequestHandler<GetAllCustomersRequest, List<Customer>>,
        IRequestHandler<CreateCustomerRequest, CreateCustomerResponse>,
        IRequestHandler<GetCustomerByIdRequest, GetCustomerByIdResponse>,
        IRequestHandler<UpdateCustomerRequest, UpdateCustomerResponse>,
        IRequestHandler<DeleteCustomerByIdRequest, DeleteCustomerByIdResponse>
    {
        public readonly ICustomerService _customerService;

        public CustomerHandler(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<List<Customer>> Handle(
           GetAllCustomersRequest request,
           CancellationToken cancellationToken)
        {
            var response = await _customerService.GetAllCustomers();

            return response;
        }

        public async Task<GetCustomerByIdResponse> Handle(
            GetCustomerByIdRequest request,
            CancellationToken cancellationToken)
        {
            var response = await _customerService.GetCustomerById(request.CustomerId);

            return response;
        }

        public async Task<CreateCustomerResponse> Handle(
            CreateCustomerRequest command,
            CancellationToken cancellationToken)
        {
            await _customerService.CreateCustomer(command);

            return new CreateCustomerResponse();
        }

        public async Task<UpdateCustomerResponse> Handle(
            UpdateCustomerRequest command,
            CancellationToken cancellationToken)
        {
            await _customerService.UpdateCustomer(command);

            return new UpdateCustomerResponse();
        }

        public async Task<DeleteCustomerByIdResponse> Handle(
            DeleteCustomerByIdRequest commad,
            CancellationToken cancellationToken)
        {
            await _customerService.DeleteCustomer(commad.CustomerId);

            return new DeleteCustomerByIdResponse();
        }
    }
}