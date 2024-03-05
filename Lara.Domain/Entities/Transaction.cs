namespace Lara.Domain.Entities;

public enum TransactionType
{
    BORROWED 
}

public class Transaction : BaseEntity
{
    public TransactionType TransactionType { get; set; }
    public ApplicationUser User { get; set; }
    public Book Book { get; set; }
    public int Quantity { get; set; }
}