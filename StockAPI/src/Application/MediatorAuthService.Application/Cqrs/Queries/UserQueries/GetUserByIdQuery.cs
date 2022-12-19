using StockAPI.Application.Dtos.UserDtos;
using StockAPI.Application.Wrappers;
using MediatR;

namespace StockAPI.Application.Cqrs.Queries.UserQueries;

public class GetUserByIdQuery : IRequest<ApiResponse<UserDto>>
{
    public Guid Id { get; private set; }

    public GetUserByIdQuery(Guid id)
    {
        Id = id;
    }
}