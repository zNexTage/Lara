using System.Text.Json.Serialization;

namespace Lara.Domain.DataTransferObjects;

public class CreateBorrowedDto
{
    public int BookId { get; set; }
    
    [JsonIgnore]
    public string? UserId { get; set; }
    
    public int Quantity { get; set; }
}