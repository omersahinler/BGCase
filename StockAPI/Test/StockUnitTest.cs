using MediatR;
using Moq;
using StockAPI.Api.Controllers.V1;

namespace Test
{
    public class StockUnitTest
    {
        [Fact]
        public void GetStockByProductCodeTest()
        {
            var service = new Mock<IMediator>();
            var controller = new StockController(service.Object);
            var resault =  controller.GetStockByProductCode("A");
            Assert.NotNull(resault);
        }
        [Fact]
        public void GetStockByVariantCodeTest()
        {
            var service = new Mock<IMediator>();
            var controller = new StockController(service.Object);
            var resault = controller.GetStockByVariantCode("A0001");
            Assert.NotNull(resault);
        }
    }
}