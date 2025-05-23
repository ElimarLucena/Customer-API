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
                @"IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'CUSTOMER')
                BEGIN
                    CREATE DATABASE CUSTOMER;
                END;",

                "USE CUSTOMER;",

                $@"IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'TB_CUSTOMERS')
                BEGIN
                    CREATE TABLE TB_CUSTOMERS (
                        CustomerId UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
                        Name NVARCHAR(255) NOT NULL,
                        Email NVARCHAR(255) NOT NULL,
                        Age INT NOT NULL,
                        Phone INT NOT NULL,
                        Document NVARCHAR(255) NOT NULL,
                        Password NVARCHAR(255) NOT NULL
                    );

                    INSERT INTO [CUSTOMER].[dbo].[TB_CUSTOMERS]
                        (CustomerId, Name, Email, Document, Phone, Age, Password)
                    VALUES 
                        ('{Guid.Parse("3b848ecb-8611-409c-b741-634f8f053ba6")}', 'TestCustomer', 'testcustomer@gmail.com', 'cpf', 123456789, 26, 'testcustomer123'),
                        ('{Guid.Parse("b5125fed-3c62-4809-af18-8e201beaf4ec")}', 'TestCustomer2', 'testcustomer2@gmail.com', 'cpf2', 123493789, 37, 'testcustomer2123'),
                        ('{Guid.Parse("79af865d-9f4b-4777-a52e-a0f46b086ddd")}', 'TestCustomer4', 'testcustomer4@gmail.com', 'cpf4', 123456789, 22, 'testcustomer4123'),
                        ('{Guid.Parse("a3fe6701-2c8a-4c7c-8309-07050fccdee7")}', 'TestCustomer5', 'testcustomer5@gmail.com', 'cpf5', 123493789, 18, 'testcustomer5123');
                END;"
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