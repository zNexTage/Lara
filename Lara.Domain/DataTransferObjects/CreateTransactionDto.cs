using Lara.Domain.Entities;

namespace Lara.Domain.DataTransferObjects;

public class CreateTransactionDto
{
    public int UserId { get; set; }
    public int BookId { get; set; }
    public int Quantity { get; set; }
}