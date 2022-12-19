using StockAPI.Application.Wrappers;
using MediatR;
using StockAPI.Application.Dtos.StockDtos;

namespace StockAPI.Application.Cqrs.Commands.StockCommands;

public class UpdateStockCommand : IRequest<ApiResponse<StockDto>>
{
    public string VariantCode { get; set; }
    public int Quantity { get; set; }
}