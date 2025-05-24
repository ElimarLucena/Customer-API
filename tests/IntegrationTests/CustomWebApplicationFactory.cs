using Infra.Data.DbContext;
using IntegrationTests.util;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Text;
using Xunit;

namespace IntegrationTests
{
    [CollectionDefinition("CustomWebApplicationFactory collection")]
    public class CustomWebApplicationFactoryCollection : ICollectionFixture<CustomWebApplicationFactory<Program>>
    {
    }

    public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram
                                                       : class
    {
        private string _connectionString { get; set; } = string.Empty;

        public CustomWebApplicationFactory()
            => _connectionString = CreateTestDataBase.GetConnectionString();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var dbContextDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(IDbConnection));
                services.Remove(dbContextDescriptor!);

                services.AddScoped<ISqlServerDataBaseContext>(db => new SqlServerDataBaseContext(_connectionString));

                // override authentication configs.
                services.Configure<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = "http://localhost:63651",
                        ValidAudience = "http://localhost:4200",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes
                        (
                            "JWTAuthenticationSecuredTest7630b55d7c954cf293283686889427ec"
                        )),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true
                    };
                });
            });
        }

        public override async ValueTask DisposeAsync()
        {
            CreateTestDataBase.StopContainerAsync();
            await base.DisposeAsync();
        }
    }
}
