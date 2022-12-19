using StockAPI.Application.Wrappers;
using MediatR;
using StockAPI.Application.Dtos.StockDtos;

namespace StockAPI.Application.Cqrs.Queries.StockQueries;

public class GetStockByProductCodeQuery : IRequest<ApiResponse<List<StockDto>>>
{
    public string ProductCode { get; private set; }

    public GetStockByProductCodeQuery(string productCode)
    {
        ProductCode = productCode;
    }
}