using System.Data;
using Microsoft.Data.SqlClient;
using Testcontainers.MsSql;

namespace IntegrationTests.util
{
    public static class CreateTestDataBase
    {
        private static MsSqlContainer? _container;

        private static string CreateContainer()
        {
            const ushort HttpPort = 1433;

            _container = new MsSqlBuilder()
                .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
                .WithEnvironment("ACCEPT_EULA", "Y")
                .WithPortBinding(HttpPort, true)
                .Build();

            _container.StartAsync().Wait();

            CreateDataBase(_container.GetConnectionString());

            return _container.GetConnectionString();
        }

        private static void CreateDataBase(string connectionString)
        {
            List<string> queries =
            [
                @"CREATE DATABASE CUSTOMER;",

                "USE CUSTOMER;",

                @"CREATE TABLE TB_CUSTOMERS (
                    CUSTOMER_ID UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
                    NAME NVARCHAR(255) NOT NULL,
                    EMAIL NVARCHAR(255) NOT NULL,
                    AGE INT NOT NULL,
                    PHONE INT NOT NULL,
                    DOCUMENT NVARCHAR(255) NOT NULL,
                    PASSWORD NVARCHAR(255) NOT NULL,
                    CREATED_AT DATETIME2 DEFAULT GETDATE(),
                    UPDATED_AT DATETIME2
                );",

                $@"INSERT INTO [CUSTOMER].[dbo].[TB_CUSTOMERS]
                    (CUSTOMER_ID, NAME, EMAIL, DOCUMENT, PHONE, AGE, PASSWORD, CREATED_AT, UPDATED_AT)
                VALUES 
                    ('{Guid.Parse("3b848ecb-8611-409c-b741-634f8f053ba6")}', 'TestCustomer', 'testcustomer@gmail.com', 'cpf', 123456789, 26, 'testcustomer123', '{DateTime.Now:yyyy-MM-dd HH:mm:ss}', '{DateTime.Now:yyyy-MM-dd HH:mm:ss}'),
                    ('{Guid.Parse("b5125fed-3c62-4809-af18-8e201beaf4ec")}', 'TestCustomer2', 'testcustomer2@gmail.com', 'cpf2', 123493789, 37, 'testcustomer2123', '{DateTime.Now:yyyy-MM-dd HH:mm:ss}', '{DateTime.Now:yyyy-MM-dd HH:mm:ss}'),
                    ('{Guid.Parse("79af865d-9f4b-4777-a52e-a0f46b086ddd")}', 'TestCustomer4', 'testcustomer4@gmail.com', 'cpf4', 123456789, 22, 'testcustomer4123', '{DateTime.Now:yyyy-MM-dd HH:mm:ss}', '{DateTime.Now:yyyy-MM-dd HH:mm:ss}'),
                    ('{Guid.Parse("a3fe6701-2c8a-4c7c-8309-07050fccdee7")}', 'TestCustomer5', 'testcustomer5@gmail.com', 'cpf5', 123493789, 18, 'testcustomer5123', '{DateTime.Now:yyyy-MM-dd HH:mm:ss}', '{DateTime.Now:yyyy-MM-dd HH:mm:ss}');"
            ];

            SqlConnection connection = new(connectionString);
            connection.Open();
            SqlCommand command = new();

            foreach (string query in queries)
            {
                command = new()
                {
                    CommandText = query,
                    CommandTimeout = 120,
                    CommandType = CommandType.Text,
                    Connection = connection
                };

                command.ExecuteNonQuery();
            }

            command.Dispose();
            connection.Dispose();
        }

        public static string GetConnectionString()
            => CreateContainer();

        public static void StopContainerAsync()
            => _container?.StopAsync().Wait();
    }
}