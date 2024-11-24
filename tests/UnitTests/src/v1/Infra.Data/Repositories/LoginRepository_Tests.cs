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
    public class LoginRepository_Tests
    {
        private readonly Mock<ISqlServerDataBaseContext> _sqlServerDataBaseContext;
        private readonly Mock<IDbConnection> _mockConnection;

        public LoginRepository_Tests()
        {
            _sqlServerDataBaseContext = new Mock<ISqlServerDataBaseContext>();
            _mockConnection = new Mock<IDbConnection>();
        }

        [Fact]
        public async Task GetCustomerByEmailPassword_Returns_Customer()
        {
            // Arrange
            List<Customer> customerDBMock = DataBaseMock.CustomerDBMock();

            _mockConnection.SetupDapperAsync(moq => moq.QuerySingleOrDefaultAsync<Customer>(It.IsAny<string>(), null, null, null, null))
                           .ReturnsAsync(customerDBMock[0]);

            _sqlServerDataBaseContext.Setup(moq => moq.Connection).Returns(_mockConnection.Object);

            LoginRepository loginRepository = new(_sqlServerDataBaseContext.Object);

            // Act
            Customer? getCustomerByEmailPassword = await loginRepository.GetCustomerByEmailPassword(customerDBMock[0].Email, customerDBMock[0].Password);

            // Assert
            getCustomerByEmailPassword.CustomerId.Should().Be(customerDBMock[0].CustomerId);
            getCustomerByEmailPassword.Name.Should().Be("User_1");
            getCustomerByEmailPassword.Email.Should().Be("User_1@gmail.com");
            getCustomerByEmailPassword.Email.Should().Match("*@*.com");
            getCustomerByEmailPassword.Age.Should().Be(20);
            getCustomerByEmailPassword.Phone.Should().Be(123456789);
            getCustomerByEmailPassword.Document.Should().Be("cpf");
            getCustomerByEmailPassword.Password.Should().Be("123");
        }

        [Fact]
        public async Task GetCustomerByEmailPassword_Returns_WithoutCustomer()
        {
            // Arrange
            List<Customer> customerDBMock = DataBaseMock.CustomerDBMock();

            _mockConnection.SetupDapperAsync(moq => moq.QuerySingleOrDefaultAsync<Customer>(It.IsAny<string>(), null, null, null, null))
                           .ReturnsAsync(() => null);

            _sqlServerDataBaseContext.Setup(moq => moq.Connection).Returns(_mockConnection.Object);

            LoginRepository loginRepository = new(_sqlServerDataBaseContext.Object);

            // Act
            Customer? getCustomerByEmailPassword = await loginRepository.GetCustomerByEmailPassword(customerDBMock[0].Email, customerDBMock[0].Password);

            // Assert
            getCustomerByEmailPassword.Should().BeNull();
        }
    }
}
