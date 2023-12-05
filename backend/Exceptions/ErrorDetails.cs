using System.Text.Json;

namespace backend.Exceptions;

public class ErrorDetails
{
    public int StatusCode { get; set; }
    public string Message { get; set; }
    
    public string ErrorType { get; set; }
    
    public ErrorDetails(int statusCode, string message, string errorType = null)
    {
        StatusCode = statusCode;
        Message = message;
        ErrorType = errorType;
    }
}
