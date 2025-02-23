using System.Data;
using Microsoft.Data.SqlClient;

namespace IntegrationTests.Util
{
    public static class CreateTestDataBase
    {
        public static void CreateDataBase(string connectionString)
        {
            List<string> queries =
            [
                "CREATE DATABASE CUSTOMER;",

                "USE CUSTOMER;",

                @"CREATE TABLE TB_CUSTOMERS (
                    CustomerId UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
                    Name NVARCHAR(255) NOT NULL,
                    Email NVARCHAR(255) NOT NULL,
                    Age INT NOT NULL,
                    Phone INT NOT NULL,
                    Document NVARCHAR(255) NOT NULL,
                    Password NVARCHAR(255) NOT NULL
                );",

                @$"INSERT INTO [CUSTOMER].[dbo].[TB_CUSTOMERS](
                    CustomerId, 
                    Name, 
                    Email, 
                    Document, 
                    Phone, 
                    Age, 
                    Password) 
                VALUES (
                    '{Guid.NewGuid()}', 
                    'TestCustomer', 
                    'testcustomer@gmail.com', 
                    'cpf', 
                    123456789, 
                    26, 
                    'testcustomer123');",

                @$"INSERT INTO [CUSTOMER].[dbo].[TB_CUSTOMERS](
                    CustomerId, 
                    Name, 
                    Email, 
                    Document, 
                    Phone, 
                    Age, 
                    Password) 
                VALUES (
                    '{Guid.NewGuid()}', 
                    'TestCustomer2', 
                    'testcustomer2@gmail.com', 
                    'cpf', 
                    123496789, 
                    37, 
                    'testcustomer2123');
                "
            ];

            SqlConnection connection = new(connectionString);
            connection.Open();
            SqlCommand command = new();

            foreach (string query in queries)
            {
                command = new()
                {
                    CommandText = query,
                    CommandTimeout = 1,
                    CommandType = CommandType.Text,
                    Connection = connection
                };

                command.ExecuteNonQuery();
            }

            command.Dispose();
            connection.Dispose();
        }
    }
}