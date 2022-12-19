using StockAPI.Application.Dtos.UserDtos;
using StockAPI.Application.Wrappers;
using MediatR;

namespace StockAPI.Application.Cqrs.Queries.UserQueries;

public class GetUserByEmailQuery : IRequest<ApiResponse<UserDto>>
{
    public string Email { get; private set; }

    public GetUserByEmailQuery(string email)
    {
        Email = email;
    }
}