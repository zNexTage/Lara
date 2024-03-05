namespace Lara.Domain.Entities;

public class Book : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public string Publisher { get; set; } = string.Empty;
    public List<string> Authors { get; set; } = default!;

    public int Quantity { get; set; } = 0;

    public decimal Price { get; set; } = 0;
}