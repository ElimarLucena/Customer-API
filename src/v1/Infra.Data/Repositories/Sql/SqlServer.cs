namespace Infra.Data.Repositories.Sql
{
    public static class SqlServer
    {
        public static string GetAllCustomers_Query() => 
            "SELECT [CustomerId], [Name], [Email], [Age], [Phone], [Document] FROM [Customer].[dbo].[TB_CUSTOMERS];";

        public static string GetCustomerById_Query() =>
            "SELECT [CustomerId], [Name], [Email], [Age], [Phone], [Document] FROM  [Customer].[dbo].[TB_CUSTOMERS] WHERE CustomerId = @customerId;";

        public static string GetCustomerByDocument_Query() =>
            "SELECT [CustomerId], [Name], [Email], [Age], [Phone], [Document] FROM [Customer].[dbo].[TB_CUSTOMERS] WHERE Document = @document;";

        public static string CreateCustomer_Command() =>
            "INSERT INTO [Customer].[dbo].[TB_CUSTOMERS](Name, Email, Document, Phone, Age, Password) VALUES (@name, @email, @document, @phone, @age, @password);";

        public static string UpdateCustomer_Command() =>
            "UPDATE [Customer].[dbo].[TB_CUSTOMERS] SET Name = @name, Email = @email, Document = @document, Phone = @phone, Age =  @age, Password = @password WHERE CustomerId = @customerId;";

        public static string DeleteCustomer_Command() =>
            "DELETE FROM [Customer].[dbo].[TB_CUSTOMERS] WHERE CustomerId = @CustomerId;";

        public static string GetCustomerByEmailPassword_Query() =>
           "SELECT [CustomerId], [Name], [Email], [Age], [Phone], [Document] FROM [Customer].[dbo].[TB_CUSTOMERS] WHERE Email = @email AND Password = @password;";
    }
}
