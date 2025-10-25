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
                "SELECT [CUSTOMER_ID] AS CustomerId, [NAME] AS Name, [EMAIL] AS Email, [AGE] AS Age, [PHONE] AS Phone, [DOCUMENT] AS Document, [CREATED_AT] AS CreatedAt, [UPDATED_AT] AS UdatedAt FROM [CUSTOMER].[dbo].[TB_CUSTOMERS];";

            // Act
            string query = SqlServer.GetAllCustomersQuery();

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
                "SELECT [CUSTOMER_ID] AS CustomerId, [NAME] AS Name, [EMAIL] AS Email, [AGE] AS Age, [PHONE] AS Phone, [DOCUMENT] AS Document, [CREATED_AT] AS CreatedAt, [UPDATED_AT] AS UdatedAt FROM [CUSTOMER].[dbo].[TB_CUSTOMERS] WHERE CUSTOMER_ID = @CUSTOMER_ID;";

            // Act
            string query = SqlServer.GetCustomerByIdQuery();

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
                "SELECT [CUSTOMER_ID] AS CustomerId, [NAME] AS Name, [EMAIL] AS Email, [AGE] AS Age, [PHONE] AS Phone, [DOCUMENT] AS Document, [CREATED_AT] AS CreatedAt, [UPDATED_AT] AS UdatedAt FROM [CUSTOMER].[dbo].[TB_CUSTOMERS] WHERE DOCUMENT = @DOCUMENT;";

            // Act
            string query = SqlServer.GetCustomerByDocumentQuery();

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
                "INSERT INTO [CUSTOMER].[dbo].[TB_CUSTOMERS](CUSTOMER_ID, NAME, EMAIL, DOCUMENT, PHONE, AGE, PASSWORD, CREATED_AT, UPDATED_AT) VALUES (@CUSTOMER_ID, @NAME, @EMAIL, @DOCUMENT, @PHONE, @AGE, @PASSWORD, @CREATED_AT, @UPDATED_AT);";

            // Act
            string command = SqlServer.CreateCustomerCommand();

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
                "UPDATE [CUSTOMER].[dbo].[TB_CUSTOMERS] SET NAME = @NAME, EMAIL = @EMAIL, DOCUMENT = @DOCUMENT, PHONE = @PHONE, AGE =  @AGE, PASSWORD = @PASSWORD, UPDATED_AT = @UPDATED_AT WHERE CUSTOMER_ID = @CUSTOMER_ID;";

            // Act
            string command = SqlServer.UpdateCustomerCommand();

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
                "DELETE FROM [CUSTOMER].[dbo].[TB_CUSTOMERS] WHERE CUSTOMER_ID = @CUSTOMER_ID;";

            // Act
            string command = SqlServer.DeleteCustomerCommand();

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
                "SELECT [CUSTOMER_ID] AS CustomerId, [NAME] AS Name, [EMAIL] AS Email, [AGE] AS Age, [PHONE] AS Phone, [DOCUMENT] AS Document, [CREATED_AT] AS CreatedAt, [UPDATED_AT] AS UdatedAt FROM [CUSTOMER].[dbo].[TB_CUSTOMERS] WHERE EMAIL = @EMAIL AND PASSWORD = @PASSWORD;";

            // Act
            string query = SqlServer.GetCustomerByEmailPasswordQuery();

            // Assert
            query.Should().Be(sqlQuery);
            query.Should().BeEquivalentTo(sqlQuery);
            query.Should().Contain(sqlQuery);
        }
    }
}
