namespace Lara.Domain.DataTransferObjects;

public class TokenDto
{
    public string Token { get; set; }
    public DateTime ExpiresIn { get; set; }
}