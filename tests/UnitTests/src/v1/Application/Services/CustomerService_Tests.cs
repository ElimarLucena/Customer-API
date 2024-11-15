using Application.Models.CustomerModels.Response;
using Application.Services;
using Domain.Entities;
using Domain.Interfaces;
using FluentAssertions;
using Moq;
using UnitTests.util;

namespace UnitTests.src.v1.Application.Services
{
    public class CustomerService_Tests
    {
        private readonly Mock<ICustomerRepository> _customerRepository;

        public CustomerService_Tests() => _customerRepository = new Mock<ICustomerRepository>();

        [Fact]
        public async Task GetAllCustomers_Returns_AllCustomers()
        {
            // Arrange
            List<Customer> customerDBMock = DataBaseMock.CustomerDBMock();

            _customerRepository.Setup(moq => moq.GetAllCustomers())
                               .ReturnsAsync(customerDBMock);

            CustomerService customerService = new(_customerRepository.Object);

            // Act
            List<GetAllCustomersResponse> getAllCustomers = await customerService.GetAllCustomers();

            // Assert
            getAllCustomers.Should().NotBeEmpty();
            getAllCustomers.Should().HaveCount(2);
            getAllCustomers.Should().ContainItemsAssignableTo<GetAllCustomersResponse>();
        }

        [Fact]
        public async Task GetAllCustomers_Returns_WithoutAllCustomers()
        {
            // Arrange
            List<Customer> customerList = [];

            _customerRepository.Setup(moq => moq.GetAllCustomers()).ReturnsAsync(customerList);

            CustomerService customerService = new(_customerRepository.Object);

            // Act
            List<GetAllCustomersResponse> getAllCustomers = await customerService.GetAllCustomers();

            // Assert
            getAllCustomers.Should().BeEmpty();
        }

        [Fact]
        public async Task GetCustomerById_Returns_Customer()
        {
            // Arrange
            List<Customer> customerDBMock = DataBaseMock.CustomerDBMock();

            Guid customerId = customerDBMock[0].CustomerId;

            _customerRepository.Setup(moq => moq.GetCustomerById(customerId))
                               .ReturnsAsync(customerDBMock[0]);

            CustomerService customerService = new(_customerRepository.Object);

            // Act
            GetCustomerByIdResponse getCustomerByIdResponse = await customerService.GetCustomerById(customerId);

            // Assert
            getCustomerByIdResponse.Should().BeOfType<GetCustomerByIdResponse>();
            getCustomerByIdResponse.CustomerId.Should().Be(customerDBMock[0].CustomerId);
            getCustomerByIdResponse.Name.Should().Be("User_1");
            getCustomerByIdResponse.Email.Should().Be("User_1@gmail.com");
            getCustomerByIdResponse.Email.Should().Match("*@*.com");
            getCustomerByIdResponse.Age.Should().Be(20);
            getCustomerByIdResponse.Phone.Should().Be(123456789);
            getCustomerByIdResponse.Document.Should().Be("cpf");
        }

        [Fact]
        public async Task GetCustomerById_Returns_Exception()
        {
            // Arrange
            Guid customerId = Guid.NewGuid();

            _customerRepository.Setup(moq => moq.GetCustomerById(customerId)).ReturnsAsync(() => null!);

            CustomerService customerService = new(_customerRepository.Object);

            // Act
            Func<Task<GetCustomerByIdResponse>> getCustomerByIdResponse = async () => await customerService.GetCustomerById(customerId);

            // Assert
            await getCustomerByIdResponse.Should()
                                         .ThrowAsync<Exception>()
                                         .WithMessage("Ops! this customer not found.");
        }
    }
}
