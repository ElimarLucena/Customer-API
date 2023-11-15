using Application.Models.LoginModels.Request;
using Application.Models.LoginModels.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Customer.API.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/login")]
    [ApiController]

    public class LoginController : Controller
    {
        private readonly IMediator _mediator;

        public LoginController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> Post(LoginCustomerRequest login, CancellationToken cancellationToken)
        {
            LoginCustomerResponse token = await _mediator.Send(login, cancellationToken);

            return Ok(token);
        }
    }
}
