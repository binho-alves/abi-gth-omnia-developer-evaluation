namespace Ambev.DeveloperEvaluation.Application.Sales.Common;

public class SaleItemModel
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice => Quantity * UnitPrice;
}
