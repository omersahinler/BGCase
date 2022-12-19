using StockAPI.Application.Dtos.UserDtos;
using StockAPI.Application.Wrappers;
using StockAPI.Domain.Core.Pagination;
using MediatR;

namespace StockAPI.Application.Cqrs.Queries.UserQueries;

public class GetUserAllQuery : PaginationParams, IRequest<ApiResponse<List<UserDto>>>
{
    public string? Name { get; set; }

    public string? Surname { get; set; }

    public string? Email { get; set; }

    public bool? IsActive { get; set; }
}