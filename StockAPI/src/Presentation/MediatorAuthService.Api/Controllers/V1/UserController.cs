using StockAPI.Api.Controllers.Base;
using StockAPI.Application.Cqrs.Commands.UserCommands;
using StockAPI.Application.Cqrs.Queries.UserQueries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StockAPI.Api.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UserController : MediatorBaseController
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _mediator.Send(new GetUserByIdQuery(id));

            return ActionResultInstance(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUser([FromQuery] GetUserAllQuery request)
        {
            var response = await _mediator.Send(request);

            return ActionResultInstance(response);
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var response = await _mediator.Send(new GetUserByEmailQuery(email));

            return ActionResultInstance(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(CreateUserCommand user)
        {
            var response = await _mediator.Send(user);

            return ActionResultInstance(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(UpdateUserCommand user)
        {
            var response = await _mediator.Send(user);

            return ActionResultInstance(response);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var response = await _mediator.Send(new DeleteUserCommand(id));

            return ActionResultInstance(response);
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordUserCommand request)
        {
            var response = await _mediator.Send(request);

            return ActionResultInstance(response);
        }
    }
}