using Xunit;
using FluentAssertions;
using System.Net;
using Newtonsoft.Json;
using Application.Models.CustomerModels.Response;
using System.Net.Http.Headers;
using IntegrationTests.Util;

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

            string token = TestTokenGenerator.GetToken();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            const string URI = "/api/v1/customer/getAllCustomers";

            // Act
            HttpResponseMessage response = await client.GetAsync(URI);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Should().NotBeNull();

            string responseContent = await response.Content.ReadAsStringAsync();
            List<GetAllCustomersResponse> contentValue = JsonConvert.DeserializeObject<List<GetAllCustomersResponse>>(responseContent)!;

            contentValue[0].CustomerId.Should().Be(Guid.Parse("3b848ecb-8611-409c-b741-634f8f053ba6"));
            contentValue[0].Name.Should().Be("TestCustomer");
            contentValue[0].Email.Should().Be("testcustomer@gmail.com");
            contentValue[0].Age.Should().Be(26);
            contentValue[0].Phone.Should().Be(123456789);
            contentValue[0].Document.Should().Be("cpf");

            contentValue[1].CustomerId.Should().Be(Guid.Parse("b5125fed-3c62-4809-af18-8e201beaf4ec"));
            contentValue[1].Name.Should().Be("TestCustomer2");
            contentValue[1].Email.Should().Be("testcustomer2@gmail.com");
            contentValue[1].Age.Should().Be(37);
            contentValue[1].Phone.Should().Be(123493789);
            contentValue[1].Document.Should().Be("cpf2");

            CreateTestDataBase.StopContainerAsync();
        }
    }
}
