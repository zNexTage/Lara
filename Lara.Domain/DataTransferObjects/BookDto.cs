using System.ComponentModel.DataAnnotations;

namespace Lara.Domain.DataTransferObjects;

public class BookDto
{
    public string Title { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public string Publisher { get; set; } = string.Empty;
    public List<string> Authors { get; set; } = default!;

    public int Quantity { get; set; } = 0;
}