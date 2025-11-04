namespace AccountingSystem.Api.Entities;

public sealed class InvoiceItem : BaseEntity
{
    public string Description { get; set; } = default!;
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public decimal LineTotal => UnitPrice * Quantity;

    public Guid InvoiceId { get; set; }
    public Invoice? Invoice { get; set; }
}
