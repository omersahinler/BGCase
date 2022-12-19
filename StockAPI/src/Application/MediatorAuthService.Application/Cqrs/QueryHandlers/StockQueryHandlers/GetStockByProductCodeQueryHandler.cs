using AutoMapper;
using FluentValidation;
using StockAPI.Application.Cqrs.Queries.StockQueries;
using StockAPI.Application.Dtos.StockDtos;
using StockAPI.Application.Wrappers;
using StockAPI.Domain.Entities;
using StockAPI.Infrastructure.Data.Context;
using StockAPI.Infrastructure.UnitOfWork;
using MediatR;
using System.Net;

namespace StockAPI.Application.Cqrs.QueryHandlers.StockQueryHandlers;

public class GetStockByProductCodeQueryHandler : IRequestHandler<GetStockByProductCodeQuery, ApiResponse<List<StockDto>>>
{
    private readonly IUnitOfWork<AppDbContext> _unitOfWork;
    private readonly IMapper _mapper;

    public GetStockByProductCodeQueryHandler(IUnitOfWork<AppDbContext> unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ApiResponse<List<StockDto>>> Handle(GetStockByProductCodeQuery request, CancellationToken cancellationToken)
    {
        var existEntity = await _unitOfWork.GetRepository<Stock>().GetAllAsync(_=>_.ProductCode == request.ProductCode);

        if (existEntity is null)
            throw new ValidationException("Stock is not found.");

        return new ApiResponse<List<StockDto>>
        {
            Data = _mapper.Map<List<StockDto>>(existEntity),
            StatusCode = (int)HttpStatusCode.OK,
            IsSuccessful = true,
        };
    }
}