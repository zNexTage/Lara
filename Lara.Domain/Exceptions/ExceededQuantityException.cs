namespace Lara.Domain.Exceptions;

public class ExceededQuantityException : Exception
{
    public ExceededQuantityException(string message): base(message)
    {
        
    }
}