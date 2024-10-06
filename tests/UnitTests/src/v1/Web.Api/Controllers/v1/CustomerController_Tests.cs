using Application.Models.CustomerModels.Request;
using Application.Models.CustomerModels.Response;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Web.Api.Controllers.v1;

namespace UnitTests.src.v1.Web.Api.Controllers.v1
{
    public class CustomerController_Tests
    {
        private readonly Mock<IMediator> _mockMediator;
        private static Guid _testId { get; set; }

        private static List<GetAllCustomerResponse> _dataBaseMock
        {
            get
            {
                return
                [
                    new GetAllCustomerResponse()
                    {
                        CustomerId = _testId,
                        Name = "User_1",
                        Email = "User_1@gmail.com",
                        Age = 20,
                        Phone = 123456789,
                        Document = "cpf"
                    },
                    new GetAllCustomerResponse()
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

        public CustomerController_Tests()
        {
            _mockMediator = new Mock<IMediator>();
            Guid guid = Guid.NewGuid();
            _testId = guid;
        }

        [Fact]
        public async Task Get_ReturnsOkResult_WithAllCustomers()
        {
            // Arrange
            CancellationToken cancellationToken = new();

            _mockMediator.Setup(moq => moq.Send(It.IsAny<GetAllCustomersRequest>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(_dataBaseMock);

            CustomerController customerController = new(_mockMediator.Object);

            // Act
            ActionResult<List<GetAllCustomerResponse>> getAllCustomers = await customerController.Get(cancellationToken);

            // Assert
            OkObjectResult response = (OkObjectResult) getAllCustomers.Result!;
            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(OkObjectResult));
            response.StatusCode.Should().Be(200);

            List<GetAllCustomerResponse> responseValue = (List<GetAllCustomerResponse>) response.Value!;
            responseValue[0].CustomerId.Should().Be(_dataBaseMock[0].CustomerId);
            responseValue[0].Name.Should().Be("User_1");
            responseValue[0].Email.Should().Be("User_1@gmail.com");
            responseValue[0].Email.Should().Match("*@*.com");
            responseValue[0].Age.Should().Be(20);
            responseValue[0].Phone.Should().Be(123456789);
            responseValue[0].Document.Should().Be("cpf");

            responseValue[1].CustomerId.Should().Be(_dataBaseMock[1].CustomerId);
            responseValue[1].Name.Should().Be("User_2");
            responseValue[1].Email.Should().Be("User_2@gmail.com");
            responseValue[1].Email.Should().Match("*@*.com");
            responseValue[1].Age.Should().Be(21);
            responseValue[1].Phone.Should().Be(123456781);
            responseValue[1].Document.Should().Be("cpf1");
        }
    }
}
