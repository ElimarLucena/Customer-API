using Application.Models.LoginModels.Request;
using Application.Models.LoginModels.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Api.Controllers.v1
{
    [ApiController]
    [Route("api/v1/login")]

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
