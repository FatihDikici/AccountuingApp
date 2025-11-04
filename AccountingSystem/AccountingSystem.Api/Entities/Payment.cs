namespace AccountingSystem.Api.Entities;

public class Payment : BaseEntity
{
    public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
    public decimal Amount { get; set; }
    public string? Method { get; set; }
    public Guid InvoiceId { get; set; }
    public Invoice? Invoice { get; set; }
}
