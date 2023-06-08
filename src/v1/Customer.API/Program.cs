using Application.Swagger;
//using Customer.API.Validators;
using FluentValidation;
using Infra.IoC.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using MediatR;
using Application.UseCases.Handlers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

//Swagger Version Url
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApiVersioning(version => 
{
    version.AssumeDefaultVersionWhenUnspecified = true;
    version.DefaultApiVersion = new ApiVersion(1, 0);
});
builder.Services.AddVersionedApiExplorer(setup =>
{
    setup.GroupNameFormat = "'v'VVV";
    setup.SubstituteApiVersionInUrl = true;
});
builder.Services.AddSwaggerGen(swagger => 
{
    swagger.OperationFilter<SwaggerDefaultValues>();
});

builder.Services.AddInfrastructureServices();
//builder.Services.AddTransient<IValidator<AddCustomerInputModel>, AddCustomerInputModelValidator>();
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerGenOptions>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(CustomerHandler)));

string connectionString = $"Server={Environment.GetEnvironmentVariable("DB_HOST")};Database={Environment.GetEnvironmentVariable("DB_NAME")};Encrypt=False;TrustServerCertificate=True;User=SA;Password={Environment.GetEnvironmentVariable("DB_SA_PASSWORD")}";
builder.Services.AddSqlServerDataBaseContext(connectionString);

var app = builder.Build();
var versionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        foreach (var description in versionDescriptionProvider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                $"Customer - {description.GroupName.ToUpper()}");
        }
    }); 
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
