using Application.Authentication;
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
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, 
                                                                   string connectionString)
        {
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<ILoginRepository, LoginRepository>();
            services.AddScoped<ILoginService, LoginService>();

            // Add Mediator.
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(CustomerHandler)));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(LoginHandler)));

            // Add Database Context.
            services.AddScoped<ISqlServerDataBaseContext>(program => new SqlServerDataBaseContext(connectionString));

            return services;
        }

        public static IServiceCollection AddSecretKeyAuthentication(this IServiceCollection services,
                                                                    string key) 
        {
            services.AddScoped<IAuthenticationToken>(program => new AuthenticationToken(key));

            return services;
        }
    }
}