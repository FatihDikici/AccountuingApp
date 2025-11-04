namespace AccountingSystem.Api.Entities;

public sealed class Customer : BaseEntity
{
    public string Name { get; set; } = default!;
    public string? Email { get; set; }
    public string? Phone { get; set; }

    public List<Invoice> Invoices { get; set; } = new();
}
