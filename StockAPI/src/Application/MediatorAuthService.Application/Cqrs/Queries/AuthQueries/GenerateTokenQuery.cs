using StockAPI.Application.Dtos.AuthDtos;
using StockAPI.Application.Dtos.UserDtos;
using StockAPI.Application.Wrappers;
using MediatR;

namespace StockAPI.Application.Cqrs.Queries.AuthQueries;

internal class GenerateTokenQuery : IRequest<ApiResponse<TokenDto>>
{
    public UserDto User { get; set; }

    public GenerateTokenQuery(UserDto user)
    {
        User = user;
    }
}