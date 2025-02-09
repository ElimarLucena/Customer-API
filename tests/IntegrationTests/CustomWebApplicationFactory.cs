using Infra.Data.DbContext;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Data;

namespace IntegrationTests
{
    public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram 
                                                       : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var dbContextDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(IDbConnection));
                services.Remove(dbContextDescriptor!);

                string connectionString = "Server=127.0.0.1,1433;Database=Customer;Encrypt=False;TrustServerCertificate=True;User=SA;Password=password@12345#";

                services.AddScoped<ISqlServerDataBaseContext>(db => new SqlServerDataBaseContext(connectionString));
            });
        }
    }
}
