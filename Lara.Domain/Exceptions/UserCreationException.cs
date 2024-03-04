namespace Lara.Domain.Exceptions;

public class UserCreationException : Exception
{
    public readonly IDictionary<string, string> Errors;

    public UserCreationException(IDictionary<string, string> errors)
    {
        Errors = errors;
    }
}