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

        private static List<GetAllCustomerResponse> _dataBaseMock
        {
            get
            {
                return
                [
                    new GetAllCustomerResponse()
                    {
                        CustomerId = 1,
                        Name = "User_1",
                        Email = "User_1@gmail.com",
                        Age = 20,
                        Phone = 123456789,
                        Document = "cpf"
                    },
                    new GetAllCustomerResponse()
                    {
                        CustomerId = 2,
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
        }

        [Fact]
        public async Task Controllers_GetAllCustomers_ReturnOK()
        {
            // Arrange
            CancellationToken cancellationToken = new();

            _mockMediator.Setup(moq => moq.Send(It.IsAny<GetAllCustomersRequest>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(_dataBaseMock);

            CustomerController customerController = new(_mockMediator.Object);

            // Act
            ActionResult<List<GetAllCustomerResponse>> getAllCustomers = await customerController.Get(cancellationToken);

            // Assert
            getAllCustomers.Result.Should().NotBeNull();
            getAllCustomers.Result.Should().BeOfType(typeof(OkObjectResult));
        }
    }
}
