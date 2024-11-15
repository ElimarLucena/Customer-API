using Application.Interfaces;
using Application.Models.LoginModels.Request;
using Application.Models.LoginModels.Response;
using Application.Services;
using Domain.Entities;
using Domain.Interfaces;
using FluentAssertions;
using Moq;
using UnitTests.util;

namespace UnitTests.src.v1.Application.Services
{
    public class LoginService_Tests
    {
        private readonly Mock<ILoginRepository> _loginRepository;
        private readonly Mock<IAuthenticationToken> _authenticationToken;

        public LoginService_Tests()
        {
            _loginRepository = new Mock<ILoginRepository>();
            _authenticationToken = new Mock<IAuthenticationToken>();
        }

        [Fact]
        public async Task GetCustomerToken_Returns_WithToken()
        {
            // Arrange
            List<Customer> customerDBMock = DataBaseMock.CustomerDBMock();

            string token = Guid.NewGuid().ToString();

            _loginRepository.Setup(moq => moq.GetCustomerByEmailPassword(It.IsAny<string>(), It.IsAny<string>()))
                            .ReturnsAsync(customerDBMock[0]);

            _authenticationToken.Setup(moq => moq.GenerateToken(It.IsAny<Customer>()))
                                .Returns(token);

            LoginCustomerRequest loginCustomerRequest = new()
            {
                Email = "test_email@gmail.com",
                Password = "23ghrut804546ade@_test"
            };

            LoginService loginService = new(_loginRepository.Object, _authenticationToken.Object);

            // Act
            LoginCustomerResponse getCustomerTokenResponse = await loginService.GetCustomerToken(loginCustomerRequest);

            // Assert
            getCustomerTokenResponse.Should().BeOfType<LoginCustomerResponse>();
            getCustomerTokenResponse.Token.Should().Be(token);
        }

        [Fact]
        public async Task GetCustomerToken_Returns_Exception()
        {
            // Arrange
            string token = Guid.NewGuid().ToString();

            _loginRepository.Setup(moq => moq.GetCustomerByEmailPassword(It.IsAny<string>(), It.IsAny<string>()))
                            .ReturnsAsync(() => null!);

            _authenticationToken.Setup(moq => moq.GenerateToken(It.IsAny<Customer>()))
                                .Returns(token);

            LoginCustomerRequest loginCustomerRequest = new()
            {
                Email = "test_email@gmail.com",
                Password = "23ghrut804546ade@_test"
            };

            LoginService loginService = new(_loginRepository.Object, _authenticationToken.Object);

            // Act
            Func<Task<LoginCustomerResponse>> getCustomerTokenResponse = async () => await loginService.GetCustomerToken(loginCustomerRequest);

            // Assert
            await getCustomerTokenResponse.Should()
                                          .ThrowAsync<Exception>()
                                          .WithMessage("Incorrect email address or password.");
        }
    }
}
