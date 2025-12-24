using Application.Interfaces;
using Application.Models.LoginModels.Request;
using Application.Models.LoginModels.Response;
using Application.UseCases.Handlers;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace UnitTests.src.v1.Application.UseCases.Handlers;

public class LoginHandler_Tests
{
    private readonly Mock<ILogger<LoginHandler>> _logger;
    private readonly Mock<ILoginService> _loginService;

    public LoginHandler_Tests()
    {
        _logger = new Mock<ILogger<LoginHandler>>();
        _loginService = new Mock<ILoginService>();
    }

    [Fact]
    public async Task Handle_Should_Return_Token()
    {
        // Arrange
        LoginCustomerRequest request = new()
        {
            Email = "test@example.com",
            Password = "password"
        };

        LoginCustomerResponse expectedResponse = new()
        {
            Token = Guid.NewGuid().ToString()
        };

        _loginService.Setup(moq => moq.GetCustomerToken(request))
                     .ReturnsAsync(expectedResponse);

        LoginHandler handler = new(_logger.Object, _loginService.Object);

        // Act
        LoginCustomerResponse loginCustomerResponse = await handler.Handle(request, CancellationToken.None);

        // Assert
        loginCustomerResponse.Should().NotBeNull();
        loginCustomerResponse.Should().BeOfType<LoginCustomerResponse>();
        loginCustomerResponse.Token.Should().Be(expectedResponse.Token);
    }
}