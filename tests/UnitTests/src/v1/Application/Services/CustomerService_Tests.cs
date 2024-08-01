using Application.Models.CustomerModels.Response;
using Application.Services;
using Domain.Entities;
using Domain.Interfaces;
using FluentAssertions;
using Moq;

namespace UnitTests.src.v1.Application.Services
{
    public class CustomerService_Tests
    {
        private readonly Mock<ICustomerRepository> _customerRepository;

        private static List<Customer> _dataBaseMock
        {
            get
            {
                return
                [
                    new Customer()
                    {
                        CustomerId = 1,
                        Name = "User_1",
                        Email = "User_1@gmail.com",
                        Age = 20,
                        Phone = 123456789,
                        Document = "cpf"
                    },
                    new Customer()
                    {
                        CustomerId = 2,
                        Name = "User_2",
                        Email = "User_2@gmail.com",
                        Age = 21,
                        Phone = 123456781,
                        Document = "cpf1"
                    },
                ];
            }
        }

        public CustomerService_Tests() => _customerRepository = new Mock<ICustomerRepository>();

        [Fact]
        public async Task GetAllCustomers_Success()
        {
            // Arrange
            _customerRepository.Setup(moq => moq.GetAllCustomers()).ReturnsAsync(_dataBaseMock);

            CustomerService customerService = new(_customerRepository.Object);

            // Act
            List<GetAllCustomerResponse> getAllCustomers = await customerService.GetAllCustomers();

            // Assert
            getAllCustomers.Should().NotBeEmpty();
            getAllCustomers.Should().HaveCount(2);
            getAllCustomers.Should().ContainItemsAssignableTo<GetAllCustomerResponse>();
        }

        [Fact]
        public async Task GetAllCustomers_Failure()
        {
            // Arrange
            List<Customer> customerList = [];
            _customerRepository.Setup(moq => moq.GetAllCustomers()).ReturnsAsync(customerList);

            CustomerService customerService = new(_customerRepository.Object);

            // Act
            List<GetAllCustomerResponse> getAllCustomers = await customerService.GetAllCustomers();

            // Assert
            getAllCustomers.Should().BeEmpty();
        }
    }
}
