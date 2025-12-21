using System.Diagnostics;
using Application.Interfaces;
using Application.Models.LoginModels.Request;
using Application.Models.LoginModels.Response;
using Infra.Data.Traces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Handlers;

public class LoginHandler(
    ILogger<LoginHandler> logger,
    ILoginService loginService
) : IRequestHandler<LoginCustomerRequest, LoginCustomerResponse>
{
    private readonly ILogger<LoginHandler> _logger = logger;
    private readonly ILoginService _loginService = loginService;

    public async Task<LoginCustomerResponse> Handle(LoginCustomerRequest request, CancellationToken cancellationToken)
    {
        using Activity? trace = Traces.ActivitySource.StartActivity("GetCustomerToken");
        trace?.SetTag("customer.email", request.Email);

        _logger.LogInformation($"class: {nameof(LoginHandler)}, method: {nameof(Handle)}, trying to login customer with email: {request.Email}.");

        LoginCustomerResponse response = await _loginService.GetCustomerToken(request);

        _logger.LogInformation($"class: {nameof(LoginHandler)}, method: {nameof(Handle)}, customer with email: {request.Email} logged in successfully.");

        return response;
    }
}