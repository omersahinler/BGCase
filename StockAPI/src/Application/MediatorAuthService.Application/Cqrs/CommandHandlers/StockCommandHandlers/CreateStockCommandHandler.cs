using AutoMapper;
using StockAPI.Application.Cqrs.Commands.StockCommands;
using StockAPI.Application.Dtos.StockDtos;
using StockAPI.Application.Exceptions;
using StockAPI.Application.Wrappers;
using StockAPI.Domain.Entities;
using StockAPI.Infrastructure.UnitOfWork;
using MediatR;
using System.Net;

namespace StockAPI.Application.Cqrs.CommandHandlers.StockComandHandlers;

public class CreateStockCommandHandler : IRequestHandler<CreateStockCommand, ApiResponse<StockDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateStockCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ApiResponse<StockDto>> Handle(CreateStockCommand request, CancellationToken cancellationToken)
    {
        bool isExistStockByVariantCode = await _unitOfWork.GetRepository<Stock>().AnyAsync(x => x.VariantCode.Equals(request.VariantCode));

        if (isExistStockByVariantCode)
            throw new BusinessException("There is a record of the variant code.");

        var StockEntity = _mapper.Map<Stock>(request);

        await _unitOfWork.GetRepository<Stock>().AddAsync(StockEntity);
        await _unitOfWork.SaveChangesAsync();

        return new ApiResponse<StockDto>
        {
            Data = _mapper.Map<StockDto>(StockEntity),
            IsSuccessful = true,
            StatusCode = (int)HttpStatusCode.Created,
            TotalItemCount = 1
        };
    }
}