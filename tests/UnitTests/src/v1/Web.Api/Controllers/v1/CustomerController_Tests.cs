using Application.Models.CustomerModels.Request;
using Application.Models.CustomerModels.Response;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using UnitTests.util;
using Web.Api.Controllers.v1;

namespace UnitTests.src.v1.Web.Api.Controllers.v1
{
    public class CustomerController_Tests
    {
        private readonly Mock<IMediator> _mockMediator;

        public CustomerController_Tests() => _mockMediator = new Mock<IMediator>();

        [Fact]
        public async Task GetAllCustomers_ReturnsOkResult_WithAllCustomers()
        {
            // Arrange
            List<GetAllCustomersResponse> getAllCustomersResponseMock = DataBaseMock.GetAllCustomersResponseMock();

            CancellationToken cancellationToken = new();

            _mockMediator.Setup(moq => moq.Send(It.IsAny<GetAllCustomersRequest>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(getAllCustomersResponseMock);

            CustomerController customerController = new(_mockMediator.Object);

            // Act
            ActionResult<List<GetAllCustomersResponse>> getAllCustomers = await customerController.GetAllCustomers(cancellationToken);

            // Assert
            OkObjectResult response = (OkObjectResult) getAllCustomers.Result!;
            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(OkObjectResult));
            response.StatusCode.Should().Be(200);

            List<GetAllCustomersResponse> responseValue = (List<GetAllCustomersResponse>) response.Value!;
            responseValue[0].CustomerId.Should().Be(getAllCustomersResponseMock[0].CustomerId);
            responseValue[0].Name.Should().Be("User_1");
            responseValue[0].Email.Should().Be("User_1@gmail.com");
            responseValue[0].Email.Should().Match("*@*.com");
            responseValue[0].Age.Should().Be(20);
            responseValue[0].Phone.Should().Be(123456789);
            responseValue[0].Document.Should().Be("cpf");

            responseValue[1].CustomerId.Should().Be(getAllCustomersResponseMock[1].CustomerId);
            responseValue[1].Name.Should().Be("User_2");
            responseValue[1].Email.Should().Be("User_2@gmail.com");
            responseValue[1].Email.Should().Match("*@*.com");
            responseValue[1].Age.Should().Be(21);
            responseValue[1].Phone.Should().Be(123456781);
            responseValue[1].Document.Should().Be("cpf1");
        }


        [Fact]
        public async Task GetCustomerById_ReturnsOkResult_WithCustomer()
        {
            // Arrange
            GetCustomerByIdResponse getCustomerByIdResponseMock = DataBaseMock.GetCustomerByIdResponseMock();

            CancellationToken cancellationToken = new();

            _mockMediator.Setup(moq => moq.Send(It.IsAny<GetCustomerByIdRequest>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(getCustomerByIdResponseMock);

            CustomerController customerController = new(_mockMediator.Object);

            // Act
            ActionResult<GetCustomerByIdResponse> GetCustomerById = await customerController.GetCustomerById(getCustomerByIdResponseMock.CustomerId, cancellationToken);

            // Assert
            OkObjectResult response = (OkObjectResult) GetCustomerById.Result!;
            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(OkObjectResult));
            response.StatusCode.Should().Be(200);

            GetCustomerByIdResponse responseValue = (GetCustomerByIdResponse) response.Value!;
            responseValue.CustomerId.Should().Be(getCustomerByIdResponseMock.CustomerId);
            responseValue.Name.Should().Be("User_1");
            responseValue.Email.Should().Be("User_1@gmail.com");
            responseValue.Email.Should().Match("*@*.com");
            responseValue.Age.Should().Be(20);
            responseValue.Phone.Should().Be(123456789);
            responseValue.Document.Should().Be("cpf");
        }
    }
}
