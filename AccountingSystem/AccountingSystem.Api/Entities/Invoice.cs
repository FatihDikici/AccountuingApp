namespace AccountingSystem.Api.Entities;

public sealed class Invoice : BaseEntity
{
    public DateTime IssueDate { get; set; } = DateTime.UtcNow;
    public DateTime DueDate { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal PaidAmount { get; set; }
    public bool IsPaid => PaidAmount >= TotalAmount;

    public Guid CustomerId { get; set; }
    public Customer? Customer { get; set; }

    public List<InvoiceItem> InvoiceItems { get; set; } = new();
    public List<Payment> Payments { get; set; } = new();

}
