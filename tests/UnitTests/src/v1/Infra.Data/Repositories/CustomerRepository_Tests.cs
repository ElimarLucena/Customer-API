using Dapper;
using Domain.Entities;
using FluentAssertions;
using Infra.Data.DbContext;
using Infra.Data.Repositories;
using Moq;
using Moq.Dapper;
using System.Data;
using UnitTests.util;

namespace UnitTests.src.v1.Infra.Data.Repositories
{
    public class CustomerRepository_Tests
    {
        private readonly Mock<ISqlServerDataBaseContext> _sqlServerDataBaseContext;
        private readonly Mock<IDbConnection> _mockConnection;

        public CustomerRepository_Tests()
        {
            _sqlServerDataBaseContext = new Mock<ISqlServerDataBaseContext>();
            _mockConnection = new Mock<IDbConnection>();
        }
        
        [Fact]
        public async Task GetAllCustomers_Returns_AllCustomers()
        {
            // Arrange
            List<Customer> customerDBMock = DataBaseMock.CustomerDBMock();

            _mockConnection.SetupDapperAsync(moq => moq.QueryAsync<Customer>(It.IsAny<string>(), null, null, null, null))
                           .ReturnsAsync(customerDBMock);

            _sqlServerDataBaseContext.Setup(moq => moq.Connection).Returns(_mockConnection.Object);

            CustomerRepository customerRepository = new(_sqlServerDataBaseContext.Object);

            // Act
            List<Customer> getAllCustomers = await customerRepository.GetAllCustomers();

            // Assert
            getAllCustomers.Should().NotBeEmpty();
            getAllCustomers.Should().HaveCount(2);
            getAllCustomers.Should().ContainItemsAssignableTo<Customer>();

            getAllCustomers[0].CustomerId.Should().Be(customerDBMock[0].CustomerId);
            getAllCustomers[0].Name.Should().Be("User_1");
            getAllCustomers[0].Email.Should().Be("User_1@gmail.com");
            getAllCustomers[0].Email.Should().Match("*@*.com");
            getAllCustomers[0].Age.Should().Be(20);
            getAllCustomers[0].Phone.Should().Be(123456789);
            getAllCustomers[0].Document.Should().Be("cpf");

            getAllCustomers[1].CustomerId.Should().Be(customerDBMock[1].CustomerId);
            getAllCustomers[1].Name.Should().Be("User_2");
            getAllCustomers[1].Email.Should().Be("User_2@gmail.com");
            getAllCustomers[1].Email.Should().Match("*@*.com");
            getAllCustomers[1].Age.Should().Be(21);
            getAllCustomers[1].Phone.Should().Be(123456781);
            getAllCustomers[1].Document.Should().Be("cpf1");
        }


        [Fact]
        public async Task GetAllCustomers_Returns_WithoutAllCustomers()
        {
            // Arrange
            List<Customer> customerDBMock = [];

            _mockConnection.SetupDapperAsync(moq => moq.QueryAsync<Customer>(It.IsAny<string>(), null, null, null, null))
                           .ReturnsAsync(customerDBMock);

            _sqlServerDataBaseContext.Setup(moq => moq.Connection).Returns(_mockConnection.Object);

            CustomerRepository customerRepository = new(_sqlServerDataBaseContext.Object);

            // Act
            List<Customer> getAllCustomers = await customerRepository.GetAllCustomers();

            // Assert
            getAllCustomers.Should().BeEmpty();
        }
    }
}
