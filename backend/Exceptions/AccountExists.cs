namespace backend.Exceptions;

public class AccountExistsException : Exception
{
    public AccountExistsException(string message) : base(message)
    {
    }
}
