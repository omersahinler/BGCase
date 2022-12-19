using StockAPI.Domain.Core.Base.Abstract;
using StockAPI.Domain.Core.Base.Concrete;

namespace StockAPI.Domain.Entities
{
    public class Stock : BaseEntity, IEntity
    {
        public string ProductCode { get; set; }
        public string VariantCode { get; set; }
        public int Quantity { get; set; }
    }
}
