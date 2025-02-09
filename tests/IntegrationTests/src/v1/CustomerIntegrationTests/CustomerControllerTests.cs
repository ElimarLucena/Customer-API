using Xunit;

namespace IntegrationTests.src.v1.CustomerIntegrationTests
{
    public class CustomerControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _factory;

        public CustomerControllerTests(CustomWebApplicationFactory<Program> factory)
            => _factory = factory;
        
        [Fact]
        public async Task GetAllCustomers_Returns_AllCustomers()
        {
            // Arrange
            HttpClient client = _factory.CreateClient();
            client.BaseAddress = new Uri("http://localhost:8080");

            const string URI = "/api/v1/customer/getAllCustomers";

            // Act
            HttpResponseMessage response = await client.GetAsync(URI);

            // Assert
            response.EnsureSuccessStatusCode();
        }
    }
}
