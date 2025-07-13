using Application.Authentication;
using Domain.Entities;
using FluentAssertions;
using UnitTests.util;
using UnitTests.util.Models.JwtModels;

namespace UnitTests.src.v1.Application.Authentication
{
    public class AuthenticationToken_Tests
    {
        [Fact]
        public void GenerateToken_Returns_WithToken()
        {
            // Arrange
            List<Customer> customerDBMock = DataBaseMock.CustomerDBMock();

            string keyTest = Guid.NewGuid().ToString();

            AuthenticationToken authenticationToken = new(keyTest);

            // Act
            string token = authenticationToken.GenerateToken(customerDBMock[0]);

            // Assert
            token.Should().NotBeNullOrEmpty();
            token.Should().BeOfType<string>();

            JwtToken jwtToken = DecodeJWT.JwtToken(token);
            jwtToken.CustomerId.Should().Be(customerDBMock[0].CustomerId);
            jwtToken.Name.Should().Be(customerDBMock[0].Name);
            jwtToken.SigningAlgorithm.Should().Be("HS256");
            jwtToken.TokenType.Should().Be("JWT");
            jwtToken.Expiration.Should().HaveDay(DateTime.UtcNow.Day);
            jwtToken.Expiration.Should().HaveMonth(DateTime.UtcNow.Month);
            jwtToken.Expiration.Should().HaveYear(DateTime.UtcNow.Year);
            jwtToken.Expiration.Should().HaveHour(DateTime.UtcNow.AddHours(1).Hour);
            jwtToken.Expiration.Should().HaveMinute(DateTime.UtcNow.AddHours(1).Minute);
        }
    }
}
