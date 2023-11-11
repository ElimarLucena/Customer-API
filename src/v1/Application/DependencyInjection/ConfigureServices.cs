using Application.Interfaces;
using Application.Services;
using Application.UseCases.Handlers;
using Domain.Interfaces;
using Infra.Data.DbContext;
using Infra.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DependencyInjection
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ICustomerService, CustomerService>();

            // Add Mediator.
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(CustomerHandler)));

            return services;
        }

        public static IServiceCollection AddSqlServerDataBaseContext(this IServiceCollection services, string connectionString)
        {
            services.AddScoped<ISqlServerDataBaseContext>(program => new SqlServerDataBaseContext(connectionString));

            return services;
        }
    }
}
