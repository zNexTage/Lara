using System.ComponentModel;

namespace Lara.Domain.Entities;

public abstract class BaseTransaction : BaseEntity
{
    public ApplicationUser User { get; set; }
    public string UserId { get; set; }
    
    public virtual Book Book { get; set; }
    public int BookId { get; set; }
    
    public int Quantity { get; set; }
}