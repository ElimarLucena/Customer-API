using Application.Interfaces;
using Application.Services;
using Domain.Interfaces;
using Infra.Data.DbContext;
using Infra.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.IoC.DependencyInjection
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ICustomerService, CustomerService>();

            return services;
        }

        public static IServiceCollection AddSqlServerDataBaseContext(this IServiceCollection services, string connectionString) 
        { 
            services.AddScoped<ISqlServerDataBaseContext>(program => new SqlServerDataBaseContext(connectionString));

            return services;
        }
    }
}
