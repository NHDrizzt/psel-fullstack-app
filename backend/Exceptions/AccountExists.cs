namespace backend.Exceptions;

public class AccountExistsException : Exception
{

    public List<ErrorDetails> ErrorTypes { get; private set; }
    
    public AccountExistsException(List<ErrorDetails> err)
    {
        ErrorTypes = err;
    }
}
