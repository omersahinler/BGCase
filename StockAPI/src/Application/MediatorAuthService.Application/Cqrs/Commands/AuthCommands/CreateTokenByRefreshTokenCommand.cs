using StockAPI.Application.Dtos.AuthDtos;
using StockAPI.Application.Wrappers;
using MediatR;

namespace StockAPI.Application.Cqrs.Commands.AuthCommands;

public class CreateTokenByRefreshTokenCommand : IRequest<ApiResponse<TokenDto>>
{
    public string RefreshToken { get; set; }

    public CreateTokenByRefreshTokenCommand(string refreshToken)
    {
        RefreshToken = refreshToken;
    }
}