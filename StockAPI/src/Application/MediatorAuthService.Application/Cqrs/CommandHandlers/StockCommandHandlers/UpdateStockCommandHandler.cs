using AutoMapper;
using FluentValidation;
using StockAPI.Application.Cqrs.Commands.StockCommands;
using StockAPI.Application.Dtos.StockDtos;
using StockAPI.Application.Wrappers;
using StockAPI.Domain.Entities;
using StockAPI.Infrastructure.UnitOfWork;
using MediatR;
using System.Net;

namespace StockAPI.Application.Cqrs.CommandHandlers.StockComandHandlers;

public class UpdateStockCommandHandler : IRequestHandler<UpdateStockCommand, ApiResponse<StockDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateStockCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ApiResponse<StockDto>> Handle(UpdateStockCommand request, CancellationToken cancellationToken)
    {
        var existStock = await _unitOfWork.GetRepository<Stock>()
            .SingleOrDefaultAsync(_=>_.VariantCode == request.VariantCode);

        if (existStock is null)
            throw new ValidationException("Stock is not found.");

        
        var mappedStock = _mapper.Map(request, existStock);

        _unitOfWork.GetRepository<Stock>().Update(mappedStock);
        await _unitOfWork.SaveChangesAsync();

        return new ApiResponse<StockDto>
        {
            Data = _mapper.Map<StockDto>(mappedStock),
            IsSuccessful = true,
            StatusCode = (int)HttpStatusCode.OK,
            TotalItemCount = 1
        };
    }
}