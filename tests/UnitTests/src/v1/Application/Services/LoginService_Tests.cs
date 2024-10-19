using Application.Interfaces;
using Application.Models.LoginModels.Request;
using Application.Models.LoginModels.Response;
using Application.Services;
using Domain.Entities;
using Domain.Interfaces;
using FluentAssertions;
using Moq;

namespace UnitTests.src.v1.Application.Services
{
    public class LoginService_Tests
    {
        private readonly Mock<ILoginRepository> _loginRepository;
        private readonly Mock<IAuthenticationToken> _authenticationToken;
        private static Guid _testId { get; set; }
        private static List<Customer> _dataBaseMock
        {
            get
            {
                return
                [
                    new Customer()
                    {
                        CustomerId = _testId,
                        Name = "User_1",
                        Email = "User_1@gmail.com",
                        Age = 20,
                        Phone = 123456789,
                        Document = "cpf"
                    },
                    new Customer()
                    {
                        CustomerId = _testId,
                        Name = "User_2",
                        Email = "User_2@gmail.com",
                        Age = 21,
                        Phone = 123456781,
                        Document = "cpf1"
                    },
                ];
            }
        }

        public LoginService_Tests()
        {
            _loginRepository = new Mock<ILoginRepository>();
            _authenticationToken = new Mock<IAuthenticationToken>();
            Guid guid = Guid.NewGuid();
            _testId = guid;
        }

        [Fact]
        public async Task GetCustomerToken_Returns_WithToken()
        {
            // Arrange
            string token = Guid.NewGuid().ToString();

            _loginRepository.Setup(moq => moq.GetCustomerByEmailPassword(It.IsAny<string>(), It.IsAny<string>()))
                            .ReturnsAsync(_dataBaseMock[0]);

            _authenticationToken.Setup(moq => moq.GenerateToken(It.IsAny<Customer>()))
                                .Returns(token);

            LoginCustomerRequest loginCustomerRequest = new()
            {
                Email = Guid.NewGuid().ToString(),
                Password = Guid.NewGuid().ToString()
            };

            LoginService loginService = new(_loginRepository.Object, _authenticationToken.Object);

            // Act
            LoginCustomerResponse getCustomerTokenResponse = await loginService.GetCustomerToken(loginCustomerRequest);

            // Assert
            getCustomerTokenResponse.Should().BeOfType<LoginCustomerResponse>();
            getCustomerTokenResponse.Token.Should().Be(token);
        }
    }
}
