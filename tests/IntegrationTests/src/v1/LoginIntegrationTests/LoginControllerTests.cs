using System.Net;
using System.Text;
using Application.Models.LoginModels.Request;
using Application.Models.LoginModels.Response;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;

namespace IntegrationTests.src.v1.LoginIntegrationTests
{
    public class LoginControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _factory;

        public LoginControllerTests(CustomWebApplicationFactory<Program> factory)
            => _factory = factory;

        [Fact]
        public async Task LoginToken_Returns_JWTToken()
        {
            // Arrange
            HttpClient client = _factory.CreateClient();

            LoginCustomerRequest loginCustomerRequest = new()
            {
                Email = "testcustomer@gmail.com",
                Password = "testcustomer123"
            };

            StringContent content = new(
                JsonConvert.SerializeObject(loginCustomerRequest),
                Encoding.UTF8, 
                "application/json"
            );

            const string URI = $"/api/v1/login/getToken";

            // Act
            HttpResponseMessage response = await client.PostAsync(URI, content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Should().NotBeNull();

            string responseContent = await response.Content.ReadAsStringAsync();
            LoginCustomerResponse loginCustomerResponse = JsonConvert.DeserializeObject<LoginCustomerResponse>(responseContent)!;
            loginCustomerResponse.Token.Should().NotBeNullOrEmpty()
                .And.BeOfType(typeof(string));
        }
    }
}
