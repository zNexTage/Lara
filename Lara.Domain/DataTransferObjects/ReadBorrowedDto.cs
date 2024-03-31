namespace Lara.Domain.DataTransferObjects;

public class ReadBorrowedDto
{
    public int Id { get; set; }
    
    public BookDto Book { get; set; }
    
    public ReadUserDto User { get; set; }
    
    public int Quantity { get; set; }
}