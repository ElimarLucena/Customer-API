using Microsoft.AspNetCore.Mvc;
using MediatR;
using Application.Models.CustomerModels.Request;
using Application.Models.CustomerModels.Response;

namespace Customer.API.Controllers.v1;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/customer")]
[ApiController]

public class CustomerController : ControllerBase
{
    private readonly IMediator _mediator;

    public CustomerController(IMediator mediator) => _mediator = mediator;

    [HttpGet("getAllCustomer")]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        GetAllCustomersRequest command = new();

        List<GetAllCustomerResponse> customers = await _mediator.Send(command, cancellationToken);

        return Ok(customers);
    }

    [HttpGet("getCustomerById/{customerId}")]
    public async Task<ActionResult> Get([FromRoute] int customerId, 
                                        CancellationToken cancellationToken)
    {
        GetCustomerByIdRequest query = new()
        {
            CustomerId = customerId
        };

        GetCustomerByIdResponse customer = await _mediator.Send(query, cancellationToken);

        return Ok(customer);
    }

    [HttpPost("createCustomer")]
    public async Task<IActionResult> Post(CreateCustomerRequest command, 
                                          CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);

        return Ok();
    }

    [HttpPut("updateCustomer")]
    public async Task<IActionResult> Put(UpdateCustomerRequest command,
                                         CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);

        return Ok();
    }

    [HttpDelete("deleteCustomerById/{customerId}")]
    public async Task<ActionResult> Delete([FromRoute] int customerId,
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
