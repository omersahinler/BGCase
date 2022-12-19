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

public class GetStockByVariantCodeQueryHandler : IRequestHandler<GetStockByVariantCodeQuery, ApiResponse<StockDto>>
{
    private readonly IUnitOfWork<AppDbContext> _unitOfWork;
    private readonly IMapper _mapper;

    public GetStockByVariantCodeQueryHandler(IUnitOfWork<AppDbContext> unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ApiResponse<StockDto>> Handle(GetStockByVariantCodeQuery request, CancellationToken cancellationToken)
    {
        var existEntity = await _unitOfWork.GetRepository<Stock>().SingleOrDefaultAsync(_=> _.VariantCode == request.VariantCode);

        if (existEntity is null)
            throw new ValidationException("Stock is not found.");

        return new ApiResponse<StockDto>
        {
            Data = _mapper.Map<StockDto>(existEntity),
            StatusCode = (int)HttpStatusCode.OK,
            IsSuccessful = true,
        };
    }
}