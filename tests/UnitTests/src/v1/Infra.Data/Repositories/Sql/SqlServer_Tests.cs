using FluentAssertions;
using Infra.Data.Repositories.Sql;

namespace UnitTests.src.v1.Infra.Data.Repositories.Sql
{
    public class SqlServer_Tests
    {
        [Fact]
        public static void Test_GetAllCustomers_Query_ShouldReturnExpectedQuery()
        {
            // Arrange
            string sqlQuery =
                "SELECT [CustomerId], [Name], [Email], [Age], [Phone], [Document] FROM [Customer].[dbo].[TB_CUSTOMERS];";

            // Act
            string query = SqlServer.GetAllCustomers_Query();

            // Assert
            query.Should().Be(sqlQuery);
            query.Should().BeEquivalentTo(sqlQuery);
            query.Should().Contain(sqlQuery);
        }

        [Fact]
        public static void Test_GetCustomerById_Query_ShouldReturnExpectedQuery()
        {
            // Arrange
            string sqlQuery =
                "SELECT [CustomerId], [Name], [Email], [Age], [Phone], [Document] FROM  [Customer].[dbo].[TB_CUSTOMERS] WHERE CustomerId = @customerId;";

            // Act
            string query = SqlServer.GetCustomerById_Query();

            // Assert
            query.Should().Be(sqlQuery);
            query.Should().BeEquivalentTo(sqlQuery);
            query.Should().Contain(sqlQuery);
        }

        [Fact]
        public static void Test_GetCustomerByDocument_Query_ShouldReturnExpectedQuery()
        {
            // Arrange
            string sqlQuery =
                "SELECT [CustomerId], [Name], [Email], [Age], [Phone], [Document] FROM [Customer].[dbo].[TB_CUSTOMERS] WHERE Document = @document;";

            // Act
            string query = SqlServer.GetCustomerByDocument_Query();

            // Assert
            query.Should().Be(sqlQuery);
            query.Should().BeEquivalentTo(sqlQuery);
            query.Should().Contain(sqlQuery);
        }

        [Fact]
        public static void Test_CreateCustomer_Command_ShouldReturnExpectedQuery()
        {
            // Arrange
            string sqlQuery =
                "INSERT INTO [Customer].[dbo].[TB_CUSTOMERS](Name, Email, Document, Phone, Age, Password) VALUES (@name, @email, @document, @phone, @age, @password);";

            // Act
            string command = SqlServer.CreateCustomer_Command();

            // Assert
            command.Should().Be(sqlQuery);
            command.Should().BeEquivalentTo(sqlQuery);
            command.Should().Contain(sqlQuery);
        }

        [Fact]
        public static void Test_UpdateCustomer_Command_ShouldReturnExpectedQuery()
        {
            // Arrange
            string sqlQuery =
                "UPDATE [Customer].[dbo].[TB_CUSTOMERS] SET Name = @name, Email = @email, Document = @document, Phone = @phone, Age =  @age, Password = @password WHERE CustomerId = @customerId;";

            // Act
            string command = SqlServer.UpdateCustomer_Command();

            // Assert
            command.Should().Be(sqlQuery);
            command.Should().BeEquivalentTo(sqlQuery);
            command.Should().Contain(sqlQuery);
        }

        [Fact]
        public static void Test_DeleteCustomer_Command_ShouldReturnExpectedQuery()
        {
            // Arrange
            string sqlQuery =
                "DELETE FROM [Customer].[dbo].[TB_CUSTOMERS] WHERE CustomerId = @CustomerId;";

            // Act
            string command = SqlServer.DeleteCustomer_Command();

            // Assert
            command.Should().Be(sqlQuery);
            command.Should().BeEquivalentTo(sqlQuery);
            command.Should().Contain(sqlQuery);
        }

        [Fact]
        public static void Test_GetCustomerByEmailPassword_Query_ShouldReturnExpectedQuery()
        {
            // Arrange
            string sqlQuery =
                "SELECT [CustomerId], [Name], [Email], [Age], [Phone], [Document] FROM [Customer].[dbo].[TB_CUSTOMERS] WHERE Email = @email AND Password = @password;";

            // Act
            string query = SqlServer.GetCustomerByEmailPassword_Query();

            // Assert
            query.Should().Be(sqlQuery);
            query.Should().BeEquivalentTo(sqlQuery);
            query.Should().Contain(sqlQuery);
        }
    }
}
