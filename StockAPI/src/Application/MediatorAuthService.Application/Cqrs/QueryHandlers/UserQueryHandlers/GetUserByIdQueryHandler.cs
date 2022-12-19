using AutoMapper;
using FluentValidation;
using StockAPI.Application.Cqrs.Queries.UserQueries;
using StockAPI.Application.Dtos.UserDtos;
using StockAPI.Application.Wrappers;
using StockAPI.Domain.Entities;
using StockAPI.Infrastructure.Data.Context;
using StockAPI.Infrastructure.UnitOfWork;
using MediatR;
using System.Net;

namespace StockAPI.Application.Cqrs.QueryHandlers.UserQueryHandlers;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, ApiResponse<UserDto>>
{
    private readonly IUnitOfWork<AppDbContext> _unitOfWork;
    private readonly IMapper _mapper;

    public GetUserByIdQueryHandler(IUnitOfWork<AppDbContext> unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ApiResponse<UserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var existEntity = await _unitOfWork.GetRepository<User>().GetByIdAsync(request.Id);

        if (existEntity is null)
            throw new ValidationException("User is not found.");

        return new ApiResponse<UserDto>
        {
            Data = _mapper.Map<UserDto>(existEntity),
            StatusCode = (int)HttpStatusCode.OK,
            IsSuccessful = true,
        };
    }
}