using AutoMapper;
using AutoMapper.QueryableExtensions;
using StockAPI.Application.Cqrs.Queries.UserQueries;
using StockAPI.Application.Dtos.UserDtos;
using StockAPI.Application.Wrappers;
using StockAPI.Domain.Core.Pagination;
using StockAPI.Domain.Entities;
using StockAPI.Infrastructure.Data.Context;
using StockAPI.Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace StockAPI.Application.Cqrs.QueryHandlers.UserQueryHandlers;

public class GetUserAllQueryHandler : IRequestHandler<GetUserAllQuery, ApiResponse<List<UserDto>>>
{
    private readonly IUnitOfWork<AppDbContext> _unitOfWork;
    private readonly IMapper _mapper;

    public GetUserAllQueryHandler(IUnitOfWork<AppDbContext> unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ApiResponse<List<UserDto>>> Handle(GetUserAllQuery request, CancellationToken cancellationToken)
    {
        var resRepo = _unitOfWork.GetRepository<User>()
            .GetAll(new PaginationParams
            {
                PageId = request.PageId,
                PageSize = request.PageSize,
                OrderKey = request.OrderKey,
                OrderType = request.OrderType
            });

        IQueryable<User> filteredData = ApplyFilter(resRepo.Item1, request.Name, request.Surname, request.Email, request.IsActive);

        var data = await filteredData
            .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new ApiResponse<List<UserDto>>
        {
            Data = _mapper.Map<List<UserDto>>(data),
            StatusCode = (int)HttpStatusCode.OK,
            IsSuccessful = true,
            TotalItemCount = resRepo.Item2
        };
    }

    private IQueryable<User> ApplyFilter(IQueryable<User> source, string? name, string? surname, string? email, bool? isActive)
    {
        if (!string.IsNullOrEmpty(name))
            source = source.Where(x => x.Name.Contains(name));

        if (!string.IsNullOrEmpty(surname))
            source = source.Where(x => x.Surname.Contains(surname));

        if (!string.IsNullOrEmpty(email))
            source = source.Where(x => x.Email.Contains(email));

        if (isActive is not null)
            source = source.Where(x => x.IsActive.Equals(isActive));

        return source;
    }
}