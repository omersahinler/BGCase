using StockAPI.Application.Wrappers;
using MediatR;
using StockAPI.Application.Dtos.StockDtos;

namespace StockAPI.Application.Cqrs.Queries.StockQueries;
public class GetStockByVariantCodeQuery : IRequest<ApiResponse<StockDto>>
{
    public string VariantCode { get; private set; }

    public GetStockByVariantCodeQuery(string variantCode)
    {
        VariantCode = variantCode;
    }
}