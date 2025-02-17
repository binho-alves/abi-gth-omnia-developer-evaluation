namespace Ambev.DeveloperEvaluation.Domain.Events;

public class SaleCreatedEvent
{
    public Guid SaleId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
