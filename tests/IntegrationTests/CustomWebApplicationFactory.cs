using Infra.Data.DbContext;
using IntegrationTests.Util;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using Testcontainers.MsSql;

namespace IntegrationTests
{
    public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram 
                                                       : class
    {
        private MsSqlContainer _container;

        public CustomWebApplicationFactory()
        {
            const ushort HttpPort = 1433;

            _container = new MsSqlBuilder()
                .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
                .WithEnvironment("ACCEPT_EULA", "Y")
                .WithPortBinding(HttpPort, true)
                .Build();

            _container.StartAsync().Wait();

            CreateTestDataBase.CreateDataBase(_container.GetConnectionString());
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var dbContextDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(IDbConnection));
                services.Remove(dbContextDescriptor!);

                services.AddScoped<ISqlServerDataBaseContext>(db => new SqlServerDataBaseContext(_container.GetConnectionString()));
            });
        }

        public void StopContainerAsync()
        {
             _container.StopAsync().Wait();
        }
    }
}
