using Application.Authentication;
using Application.Interfaces;
using Domain.Entities;
using FluentAssertions;
using Moq;
using UnitTests.util;

namespace UnitTests.src.v1.Application.Authentication
{
    public class AuthenticationToken_Tests
    {
        private readonly Mock<IAuthenticationToken> _authenticationToken;

        public AuthenticationToken_Tests() => _authenticationToken = new Mock<IAuthenticationToken>();

        [Fact]
        public void GenerateToken_Returns_WithToken()
        {
            // Arrange
            List<Customer> customerDBMock = DataBaseMock.CustomerDBMock();

            string keyTest = Guid.NewGuid().ToString();

            AuthenticationToken authenticationToken = new(keyTest);

            // Act
            string generateToken = authenticationToken.GenerateToken(customerDBMock[0]);

            // Assert
            generateToken.Should().NotBeNullOrEmpty();
            generateToken.Should().BeOfType<string>();
        }
    }
}
