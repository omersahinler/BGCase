using StockAPI.Api.Controllers.Base;
using StockAPI.Application.Cqrs.Commands.AuthCommands;
using StockAPI.Application.Cqrs.Queries.AuthQueries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StockAPI.Api.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AuthController : MediatorBaseController
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> CreateToken(CreateTokenQuery request)
        {
            var response = await _mediator.Send(request);

            return ActionResultInstance(response);
        }

        [Authorize]
        [HttpPost("refresh-token")]
        public async Task<IActionResult> CreateTokenByRefreshToken(CreateTokenByRefreshTokenCommand request)
        {
            var response = await _mediator.Send(request);

            return ActionResultInstance(response);
        }
    }
}
