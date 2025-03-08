using Infra.Data.DbContext;
using IntegrationTests.Util;
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

                services.AddScoped<ISqlServerDataBaseContext>(db => new SqlServerDataBaseContext(CreateTestDataBase.GetConnectionString()));
            });
        }
    }
}
