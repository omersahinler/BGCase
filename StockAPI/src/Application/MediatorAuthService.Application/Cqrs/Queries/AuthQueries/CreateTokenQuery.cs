using StockAPI.Application.Dtos.AuthDtos;
using StockAPI.Application.Wrappers;
using MediatR;

namespace StockAPI.Application.Cqrs.Queries.AuthQueries;

public class CreateTokenQuery : IRequest<ApiResponse<TokenDto>>
{
	public string Email { get; set; }

	public string Password { get; set; }
}