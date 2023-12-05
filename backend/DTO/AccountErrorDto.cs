using backend.Exceptions;

namespace backend.DTO;

public class AccountErrorDto
{
    public ErrorDetails ErrorDetails { get; set; } = null!;
}