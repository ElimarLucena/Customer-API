namespace Infra.Data.Repositories.Sql
{
    public static class SqlServer
    {
        public static string GetAllCustomersQuery() => 
            "SELECT [CUSTOMER_ID] AS CustomerId, [NAME] AS Name, [EMAIL] AS Email, [AGE] AS Age, [PHONE] AS Phone, [DOCUMENT] AS Document, [CREATED_AT] AS CreatedAt, [UPDATED_AT] AS UdatedAt FROM [CUSTOMER].[dbo].[TB_CUSTOMERS];";

        public static string GetCustomerByIdQuery() =>
            "SELECT [CUSTOMER_ID] AS CustomerId, [NAME] AS Name, [EMAIL] AS Email, [AGE] AS Age, [PHONE] AS Phone, [DOCUMENT] AS Document, [CREATED_AT] AS CreatedAt, [UPDATED_AT] AS UdatedAt FROM [CUSTOMER].[dbo].[TB_CUSTOMERS] WHERE CUSTOMER_ID = @CUSTOMER_ID;";

        public static string GetCustomerByDocumentQuery() =>
            "SELECT [CUSTOMER_ID] AS CustomerId, [NAME] AS Name, [EMAIL] AS Email, [AGE] AS Age, [PHONE] AS Phone, [DOCUMENT] AS Document, [CREATED_AT] AS CreatedAt, [UPDATED_AT] AS UdatedAt FROM [CUSTOMER].[dbo].[TB_CUSTOMERS] WHERE DOCUMENT = @DOCUMENT;";

        public static string CreateCustomerCommand() =>
            "INSERT INTO [CUSTOMER].[dbo].[TB_CUSTOMERS](CUSTOMER_ID, NAME, EMAIL, DOCUMENT, PHONE, AGE, PASSWORD, CREATED_AT, UPDATED_AT) VALUES (@CUSTOMER_ID, @NAME, @EMAIL, @DOCUMENT, @PHONE, @AGE, @PASSWORD, @CREATED_AT, @UPDATED_AT);";

        public static string UpdateCustomerCommand() =>
            "UPDATE [CUSTOMER].[dbo].[TB_CUSTOMERS] SET NAME = @NAME, EMAIL = @EMAIL, DOCUMENT = @DOCUMENT, PHONE = @PHONE, AGE =  @AGE, PASSWORD = @PASSWORD, UPDATED_AT = @UPDATED_AT WHERE CUSTOMER_ID = @CUSTOMER_ID;";

        public static string DeleteCustomerCommand() =>
            "DELETE FROM [CUSTOMER].[dbo].[TB_CUSTOMERS] WHERE CUSTOMER_ID = @CUSTOMER_ID;";

        public static string GetCustomerByEmailPasswordQuery() =>
           "SELECT [CUSTOMER_ID] AS CustomerId, [NAME] AS Name, [EMAIL] AS Email, [AGE] AS Age, [PHONE] AS Phone, [DOCUMENT] AS Document, [CREATED_AT] AS CreatedAt, [UPDATED_AT] AS UdatedAt FROM [CUSTOMER].[dbo].[TB_CUSTOMERS] WHERE EMAIL = @EMAIL AND PASSWORD = @PASSWORD;";
    }
}
