using StockAPI.Application.Dtos.UserDtos;
using StockAPI.Application.Wrappers;
using MediatR;

namespace StockAPI.Application.Cqrs.Commands.UserCommands;

public class CreateUserCommand : IRequest<ApiResponse<UserDto>>
{
    public string Name { get; set; }

    public string Surname { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }
}