using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Sale : BaseEntity
{
    public string SaleNumber { get; set; }
    public Guid CustomerId { get; set; }
    public decimal TotalAmount { get; private set; }
    public SaleStatus Status { get; set; }
    public List<SaleItem> Items { get; private set; } = new();

    public Sale()
    {
    }

    public Sale(Guid customerId, List<SaleItem> items)
    {
        SaleNumber = Guid.NewGuid().ToString();
        CustomerId = customerId;
        Status = SaleStatus.Pending;
        Items = items;

        ApplyDiscountRules();
        CalculateTotalAmount();
    }

    public void UpdateItems(List<SaleItem> newItems)
    {
        Items = newItems;
        ApplyDiscountRules();
        CalculateTotalAmount();
    }

    private void ApplyDiscountRules()
    {
        foreach (var item in Items)
        {
            if (item.Quantity > 20)
            {
                throw new InvalidOperationException($"Não é permitido vender mais de 20 unidades do item {item.ProductId}");
            }
            else if (item.Quantity >= 10)
            {
                item.ApplyDiscount(0.20m);
            }
            else if (item.Quantity >= 4)
            {
                item.ApplyDiscount(0.10m);
            }
        }
    }

    private void CalculateTotalAmount()
    {
        TotalAmount = Items.Sum(i => i.TotalPrice);
    }
}
