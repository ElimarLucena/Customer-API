using Application.Models.LoginModels.Request;
using Application.Models.LoginModels.Response;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Web.Api.Controllers.v1;

namespace UnitTests.src.v1.Web.Api.Controllers.v1
{
    public class LoginController_Tests
    {
        private readonly Mock<IMediator> _mockMediator;

        public LoginController_Tests() => _mockMediator = new Mock<IMediator>();

        [Fact]
        public async Task LoginToken_ReturnsOkResult_WithToken()
        {
            // Arrange
            CancellationToken cancellationToken = new();

            LoginCustomerRequest loginTest = new () 
            { 
                Email = "test_email@gmail.com", 
                Password = "23ghrut804546ade@_test" 
            };

            LoginCustomerResponse tokenTest = new() 
            { 
                Token = Guid.NewGuid().ToString() 
            };

            _mockMediator.Setup(moq => moq.Send(It.IsAny<LoginCustomerRequest>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(tokenTest);

            LoginController loginController = new(_mockMediator.Object);

            // Act
            ActionResult<LoginCustomerResponse> getLoginToken = await loginController.Login(loginTest, cancellationToken);

            // Assert
            OkObjectResult response = (OkObjectResult) getLoginToken.Result!;
            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(OkObjectResult));
            response.StatusCode.Should().Be(200);

            LoginCustomerResponse responseValue = (LoginCustomerResponse) response.Value!;
            responseValue.Token.Should().NotBeNull();
            responseValue.Token.Should().Be(tokenTest.Token);
        }
    }
}
