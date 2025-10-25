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

        [Fact]
        public async Task GetCustomerById_Returns_Customer()
        {
            // Arrange
            List<Customer> customerDBMock = DataBaseMock.CustomerDBMock();

            _mockConnection.SetupDapperAsync(moq => moq.QuerySingleOrDefaultAsync<Customer>(It.IsAny<string>(), null, null, null, null))
                           .ReturnsAsync(customerDBMock[0]);

            _sqlServerDataBaseContext.Setup(moq => moq.Connection).Returns(_mockConnection.Object);

            CustomerRepository customerRepository = new(_sqlServerDataBaseContext.Object);

            // Act
            Customer? getCustomerById = await customerRepository.GetCustomerById(customerDBMock[0].CustomerId);

            // Assert
            getCustomerById.CustomerId.Should().Be(customerDBMock[0].CustomerId);
            getCustomerById.Name.Should().Be("User_1");
            getCustomerById.Email.Should().Be("User_1@gmail.com");
            getCustomerById.Email.Should().Match("*@*.com");
            getCustomerById.Age.Should().Be(20);
            getCustomerById.Phone.Should().Be(123456789);
            getCustomerById.Document.Should().Be("cpf");
            getCustomerById.Password.Should().Be("123");
        }

        [Fact]
        public async Task GetCustomerById_Returns_WithoutCustomer()
        {
            // Arrange
            List<Customer> customerDBMock = DataBaseMock.CustomerDBMock();

            _mockConnection.SetupDapperAsync(moq => moq.QuerySingleOrDefaultAsync<Customer>(It.IsAny<string>(), null, null, null, null))
                           .ReturnsAsync(() => null);

            _sqlServerDataBaseContext.Setup(moq => moq.Connection).Returns(_mockConnection.Object);

            CustomerRepository customerRepository = new(_sqlServerDataBaseContext.Object);

            // Act
            Customer? getCustomerById = await customerRepository.GetCustomerById(customerDBMock[0].CustomerId);

            // Assert
            getCustomerById.Should().BeNull();
        }

        [Fact]
        public async Task GetCustomerByDocument_Returns_Customer()
        {
            // Arrange
            List<Customer> customerDBMock = DataBaseMock.CustomerDBMock();

            string document = "cpf";

            _mockConnection.SetupDapperAsync(moq => moq.QuerySingleOrDefaultAsync<Customer>(It.IsAny<string>(), null, null, null, null))
                           .ReturnsAsync(customerDBMock[0]);

            _sqlServerDataBaseContext.Setup(moq => moq.Connection).Returns(_mockConnection.Object);

            CustomerRepository customerRepository = new(_sqlServerDataBaseContext.Object);

            // Act
            Customer? getCustomerByDocument = await customerRepository.GetCustomerByDocument(document);

            // Assert
            getCustomerByDocument.CustomerId.Should().Be(customerDBMock[0].CustomerId);
            getCustomerByDocument.Name.Should().Be("User_1");
            getCustomerByDocument.Email.Should().Be("User_1@gmail.com");
            getCustomerByDocument.Email.Should().Match("*@*.com");
            getCustomerByDocument.Age.Should().Be(20);
            getCustomerByDocument.Phone.Should().Be(123456789);
            getCustomerByDocument.Document.Should().Be("cpf");
            getCustomerByDocument.Password.Should().Be("123");
        }

        [Fact]
        public async Task GetCustomerByDocument_Returns_WithoutCustomer()
        {
            // Arrange
            string document = "cpf";

            _mockConnection.SetupDapperAsync(moq => moq.QuerySingleOrDefaultAsync<Customer>(It.IsAny<string>(), null, null, null, null))
                           .ReturnsAsync(() => null);

            _sqlServerDataBaseContext.Setup(moq => moq.Connection).Returns(_mockConnection.Object);

            CustomerRepository customerRepository = new(_sqlServerDataBaseContext.Object);

            // Act
            Customer? getCustomerByDocument = await customerRepository.GetCustomerByDocument(document);

            // Assert
            getCustomerByDocument.Should().BeNull();
        }

        [Fact]
        public async Task CreateCustomer_With_Success()
        {
            // Arrange
            List<Customer> customerDBMock = DataBaseMock.CustomerDBMock();

            int numberLinesChanged = 1;

            _mockConnection.SetupDapperAsync(moq => moq.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>(), null, null, null))
                           .ReturnsAsync(numberLinesChanged);

            _sqlServerDataBaseContext.Setup(moq => moq.Connection).Returns(_mockConnection.Object);

            CustomerRepository customerRepository = new(_sqlServerDataBaseContext.Object);

            // Act
            int response = await customerRepository.CreateCustomer(customerDBMock[0]);

            // Assert
            response.Should().Be(1);
        }

        [Fact]
        public async Task CreateCustomer_Without_Success()
        {
            // Arrange
            List<Customer> customerDBMock = DataBaseMock.CustomerDBMock();

            int numberLinesChanged = 0;

            _mockConnection.SetupDapperAsync(moq => moq.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>(), null, null, null))
                           .ReturnsAsync(numberLinesChanged);

            _sqlServerDataBaseContext.Setup(moq => moq.Connection).Returns(_mockConnection.Object);

            CustomerRepository customerRepository = new(_sqlServerDataBaseContext.Object);

            // Act
            int response = await customerRepository.CreateCustomer(customerDBMock[0]);

            // Assert
            response.Should().Be(0);
        }

        [Fact]
        public async Task UpdateCustomer_With_Success()
        {
            // Arrange
            List<Customer> customerDBMock = DataBaseMock.CustomerDBMock();

            int numberLinesChanged = 1;

            _mockConnection.SetupDapperAsync(moq => moq.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>(), null, null, null))
                           .ReturnsAsync(numberLinesChanged);

            _sqlServerDataBaseContext.Setup(moq => moq.Connection).Returns(_mockConnection.Object);

            CustomerRepository customerRepository = new(_sqlServerDataBaseContext.Object);

            // Act
            int response = await customerRepository.UpdateCustomer(customerDBMock[0]);

            // Assert
            response.Should().Be(1);
        }

        [Fact]
        public async Task UpdateCustomer_Without_Success()
        {
            // Arrange
            List<Customer> customerDBMock = DataBaseMock.CustomerDBMock();

            int numberLinesChanged = 0;

            _mockConnection.SetupDapperAsync(moq => moq.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>(), null, null, null))
                           .ReturnsAsync(numberLinesChanged);

            _sqlServerDataBaseContext.Setup(moq => moq.Connection).Returns(_mockConnection.Object);

            CustomerRepository customerRepository = new(_sqlServerDataBaseContext.Object);

            // Act
            int response = await customerRepository.UpdateCustomer(customerDBMock[0]);

            // Assert
            response.Should().Be(0);
        }

        [Fact]
        public async Task DeleteCustomer_With_Success()
        {
            // Arrange
            List<Customer> customerDBMock = DataBaseMock.CustomerDBMock();

            int numberLinesChanged = 1;

            _mockConnection.SetupDapperAsync(moq => moq.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>(), null, null, null))
                           .ReturnsAsync(numberLinesChanged);

            _sqlServerDataBaseContext.Setup(moq => moq.Connection).Returns(_mockConnection.Object);

            CustomerRepository customerRepository = new(_sqlServerDataBaseContext.Object);

            // Act
            int response = await customerRepository.DeleteCustomer(customerDBMock[0].CustomerId);

            // Assert
            response.Should().Be(1);
        }

        [Fact]
        public async Task DeleteCustomer_Without_Success()
        {
            // Arrange
            List<Customer> customerDBMock = DataBaseMock.CustomerDBMock();

            int numberLinesChanged = 0;

            _mockConnection.SetupDapperAsync(moq => moq.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>(), null, null, null))
                           .ReturnsAsync(numberLinesChanged);

            _sqlServerDataBaseContext.Setup(moq => moq.Connection).Returns(_mockConnection.Object);

            CustomerRepository customerRepository = new(_sqlServerDataBaseContext.Object);

            // Act
            int response = await customerRepository.DeleteCustomer(customerDBMock[0].CustomerId);

            // Assert
            response.Should().Be(0);
        }
    }
}
