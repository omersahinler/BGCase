using StockAPI.Application.Dtos.ResponseDtos;
using StockAPI.Application.Wrappers;
using MediatR;

namespace StockAPI.Application.Cqrs.Commands.UserCommands;

public class DeleteUserCommand : IRequest<ApiResponse<NoDataDto>>
{
    public Guid Id { get; private set; }

    public DeleteUserCommand(Guid id)
    {
        Id = id;
    }
}