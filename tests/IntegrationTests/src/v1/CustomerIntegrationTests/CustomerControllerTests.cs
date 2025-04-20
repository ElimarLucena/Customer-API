using Xunit;
using FluentAssertions;
using System.Net;
using Newtonsoft.Json;
using Application.Models.CustomerModels.Response;
using System.Net.Http.Headers;
using IntegrationTests.Util;
using Application.Models.CustomerModels.Request;
using System.Text;

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
            List<GetAllCustomersResponse> getAllCustomersResponse = JsonConvert.DeserializeObject<List<GetAllCustomersResponse>>(responseContent)!;
            getAllCustomersResponse.Should().NotBeNullOrEmpty()
                .And.HaveCountGreaterThanOrEqualTo(4)
                .And.ContainItemsAssignableTo<GetAllCustomersResponse>();
        }

        [Fact]
        public async Task GetCustomerById_Returns_Customer()
        {
            // Arrange
            HttpClient client = _factory.CreateClient();

            string token = TestTokenGenerator.GetToken();

            Guid customerId = Guid.Parse("3b848ecb-8611-409c-b741-634f8f053ba6");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            string URI = $"/api/v1/customer/getCustomerById/{customerId}";

            // Act
            HttpResponseMessage response = await client.GetAsync(URI);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Should().NotBeNull();

            string responseContent = await response.Content.ReadAsStringAsync();
            GetCustomerByIdResponse contentValue = JsonConvert.DeserializeObject<GetCustomerByIdResponse>(responseContent)!;

            contentValue.CustomerId.Should().Be(Guid.Parse("3b848ecb-8611-409c-b741-634f8f053ba6"));
            contentValue.Name.Should().Be("TestCustomer");
            contentValue.Email.Should().Be("testcustomer@gmail.com");
            contentValue.Age.Should().Be(26);
            contentValue.Phone.Should().Be(123456789);
            contentValue.Document.Should().Be("cpf");
        }

        [Fact]
        public async Task CreateCustomer_Returns_OkResult()
        {
            // Arrange
            HttpClient client = _factory.CreateClient();

            CreateCustomerRequest createCustomerRequest = new()
            {
                Name = "TestCustomer3",
                Email = "testcustomer3@gmail.com",
                Age = 38,
                Phone = 123493789,
                Document = "cpf3",
                Password = "testcustomer3123"
            };

            StringContent content = new(
                JsonConvert.SerializeObject(createCustomerRequest), 
                Encoding.UTF8, 
                "application/json"
            );

            string URI = $"/api/v1/customer/createCustomer";

            // Act
            HttpResponseMessage response = await client.PostAsync(URI, content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task UpdateCustomer_Returns_OkResult()
        {
            // Arrange
            HttpClient client = _factory.CreateClient();

            string token = TestTokenGenerator.GetToken();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            UpdateCustomerRequest updateCustomerRequest = new()
            {
                CustomerId = Guid.Parse("79af865d-9f4b-4777-a52e-a0f46b086ddd"),
                Name = "TestCustomer44",
                Email = "testcustomer44@gmail.com",
                Age = 44,
                Phone = 123493744,
                Document = "cpf44",
                Password = "testcustomer44123"
            };

            StringContent content = new(
                JsonConvert.SerializeObject(updateCustomerRequest), 
                Encoding.UTF8, 
                "application/json"
            );

            string URI = $"/api/v1/customer/updateCustomer";

            // Act
            HttpResponseMessage response = await client.PutAsync(URI, content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task DeleteCustomerById_Returns_OkResult()
        {
            // Arrange
            HttpClient client = _factory.CreateClient();

            string token = TestTokenGenerator.GetToken();

            Guid customerId = Guid.Parse("a3fe6701-2c8a-4c7c-8309-07050fccdee7");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            string URI = $"/api/v1/customer/deleteCustomerById/{customerId}";

            // Act
            HttpResponseMessage response = await client.DeleteAsync(URI);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
