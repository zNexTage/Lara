namespace Lara.Domain.Entities;

public class Book : BaseEntity
{
    public string Title { get; set; }
    public string Image { get; set; }
    public string Publisher { get; set; }
    public List<string> Authors { get; set; }
}