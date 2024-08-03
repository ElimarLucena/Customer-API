using Application.Models.CustomerModels.Response;
using Castle.Core.Resource;
using Dapper;
using Domain.Entities;
using FluentAssertions;
using Infra.Data.DbContext;
using Infra.Data.Repositories;
using Moq;
using Moq.Dapper;
using System.Data;

namespace UnitTests.src.v1.Infra.Data.Repositories
{
    public class CustomerRepository_Tests
    {
        private readonly Mock<ISqlServerDataBaseContext> _sqlServerDataBaseContext;
        private readonly Mock<IDbConnection> _mockConnection;

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

        public CustomerRepository_Tests()
        {
            _sqlServerDataBaseContext = new Mock<ISqlServerDataBaseContext>();
            _mockConnection = new Mock<IDbConnection>();
        }
        
        [Fact]
        public async Task GetAllCustomers_Success()
        {
            // Arrange
            _mockConnection.SetupDapperAsync(moq => moq.QueryAsync<Customer>(It.IsAny<string>(), null, null, null, null))
                                                       .ReturnsAsync(_dataBaseMock);

            _sqlServerDataBaseContext.Setup(moq => moq.Connection).Returns(_mockConnection.Object);

            CustomerRepository customerRepository = new(_sqlServerDataBaseContext.Object);

            // Act
            List<Customer> getAllCustomers = await customerRepository.GetAllCustomers();

            // Assert
            getAllCustomers.Should().NotBeEmpty();
            getAllCustomers.Should().HaveCount(2);
            getAllCustomers.Should().ContainItemsAssignableTo<Customer>();

            getAllCustomers[0].CustomerId.Should().Be(1);
            getAllCustomers[0].Name.Should().Be("User_1");
            getAllCustomers[0].Email.Should().Be("User_1@gmail.com");
            getAllCustomers[0].Email.Should().Match("*@*.com");
            getAllCustomers[0].Age.Should().Be(20);
            getAllCustomers[0].Phone.Should().Be(123456789);
            getAllCustomers[0].Document.Should().Be("cpf");

            getAllCustomers[1].CustomerId.Should().Be(2);
            getAllCustomers[1].Name.Should().Be("User_2");
            getAllCustomers[1].Email.Should().Be("User_2@gmail.com");
            getAllCustomers[1].Email.Should().Match("*@*.com");
            getAllCustomers[1].Age.Should().Be(21);
            getAllCustomers[1].Phone.Should().Be(123456781);
            getAllCustomers[1].Document.Should().Be("cpf1");
        }


        [Fact]
        public async Task GetAllCustomers_Failure()
        {
            // Arrange
            List<Customer> customerList = [];

            _mockConnection.SetupDapperAsync(moq => moq.QueryAsync<Customer>(It.IsAny<string>(), null, null, null, null))
                                                       .ReturnsAsync(customerList);

            _sqlServerDataBaseContext.Setup(moq => moq.Connection).Returns(_mockConnection.Object);

            CustomerRepository customerRepository = new(_sqlServerDataBaseContext.Object);

            // Act
            List<Customer> getAllCustomers = await customerRepository.GetAllCustomers();

            // Assert
            getAllCustomers.Should().BeEmpty();
        }
    }
}
