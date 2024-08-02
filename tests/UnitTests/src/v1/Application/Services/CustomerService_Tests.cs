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

        [Fact]
        public async Task GetCustomerById_Success()
        {
            // Arrange
            int customerId = 1;
            _customerRepository.Setup(moq => moq.GetCustomerById(customerId)).ReturnsAsync(_dataBaseMock[0]);

            CustomerService customerService = new(_customerRepository.Object);

            // Act
            GetCustomerByIdResponse getCustomerByIdResponse = await customerService.GetCustomerById(customerId);

            // Assert
            getCustomerByIdResponse.Should().BeOfType<GetCustomerByIdResponse>();
            getCustomerByIdResponse.CustomerId.Should().Be(customerId);
            getCustomerByIdResponse.Name.Should().Be("User_1");
            getCustomerByIdResponse.Email.Should().Be("User_1@gmail.com");
            getCustomerByIdResponse.Email.Should().Match("*@*.com");
            getCustomerByIdResponse.Age.Should().Be(20);
            getCustomerByIdResponse.Phone.Should().Be(123456789);
            getCustomerByIdResponse.Document.Should().Be("cpf");
        }

        [Fact]
        public async Task GetCustomerById_Failure()
        {
            // Arrange
            int customerId = 3;
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
