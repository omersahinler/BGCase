namespace StockAPI.Application.Dtos.StockDtos;

public class StockDto
{
    public Guid Id { get; set; }

    public string ProductCode { get; set; }
    public string VariantCode { get; set; }
    public int Quantity { get; set; }

    public bool IsActive { get; set; }
}