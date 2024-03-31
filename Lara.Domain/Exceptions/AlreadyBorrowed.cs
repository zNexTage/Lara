namespace Lara.Domain.Exceptions;

public class AlreadyBorrowed : Exception
{
    public AlreadyBorrowed(string message) : base(message)
    {
        
    }
}