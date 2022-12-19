using StockAPI.Api.Controllers.Base;
using StockAPI.Application.Cqrs.Commands.StockCommands;
using StockAPI.Application.Cqrs.Queries.StockQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace StockAPI.Api.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class StockController : MediatorBaseController
    {
        private readonly IMediator _mediator;

        public StockController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("{variantCode}/variant")]

        public async Task<IActionResult> GetStockByVariantCode(string variantCode)
        {
            var response = await _mediator.Send(new GetStockByVariantCodeQuery(variantCode));

            return ActionResultInstance(response);
        }
        [HttpGet]
        [Route("{productCode}/product")]

        public async Task<IActionResult> GetStockByProductCode(string productCode)
        {
            var response = await _mediator.Send(new GetStockByProductCodeQuery(productCode));

            return ActionResultInstance(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddStock(CreateStockCommand Stock)
        {
            var response = await _mediator.Send(Stock);

            return ActionResultInstance(response);
        }

        [HttpPut]
        [Route("{variantCode}")]
        public async Task<IActionResult> UpdateStock(string variantCode, int quantity)
        {
            var response = await _mediator.Send(new UpdateStockCommand() { Quantity=quantity,VariantCode=variantCode});

            return ActionResultInstance(response);
        }
    }
}