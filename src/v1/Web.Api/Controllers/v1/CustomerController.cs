using Microsoft.AspNetCore.Mvc;
using MediatR;
using Application.Models.CustomerModels.Request;
using Application.Models.CustomerModels.Response;
using Microsoft.AspNetCore.Authorization;

namespace Web.Api.Controllers.v1;

[ApiController]
[Authorize]
[Route("api/v1/customer")]

public class CustomerController : ControllerBase
{
    private readonly IMediator _mediator;

    public CustomerController(IMediator mediator) => _mediator = mediator;

    [Authorize(Roles = "admin,manager")]
    [HttpGet("getAllCustomers")]
    public async Task<ActionResult<List<GetAllCustomersResponse>>> GetAllCustomers(CancellationToken cancellationToken)
    {
        GetAllCustomersRequest query = new();

        List<GetAllCustomersResponse> customers = await _mediator.Send(query, cancellationToken);

        return Ok(customers);
    }

    [HttpGet("getCustomerById/{customerId:guid}")]
    public async Task<ActionResult<GetCustomerByIdResponse>> GetCustomerById([FromRoute] Guid customerId, 
                                                                             CancellationToken cancellationToken)
    {
        GetCustomerByIdRequest query = new()
        {
            CustomerId = customerId
        };

        GetCustomerByIdResponse customer = await _mediator.Send(query, cancellationToken);

        return Ok(customer);
    }

    [AllowAnonymous]
    [HttpPost("createCustomer")]
    public async Task<ActionResult> CreateCustomer(CreateCustomerRequest command, 
                                                   CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);

        return Ok();
    }

    [HttpPut("updateCustomer")]
    public async Task<ActionResult> UpdateCustomer(UpdateCustomerRequest command,
                                                   CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);

        return Ok();
    }

    [HttpDelete("deleteCustomerById/{customerId:guid}")]
    public async Task<ActionResult> DeleteCustomerById([FromRoute] Guid customerId,
                                                       CancellationToken cancellationToken)
    {
        DeleteCustomerByIdRequest command = new()
        {
            CustomerId = customerId
        };

        await _mediator.Send(command, cancellationToken);

        return Ok();
    }
}
