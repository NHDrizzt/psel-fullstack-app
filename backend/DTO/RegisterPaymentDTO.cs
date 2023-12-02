namespace backend.DTO;

public class RegisterPaymentDto
{
    public int IdAccount { get; set; }
    public decimal Value { get; set; }
    public DateTime TransactionDate { get; set; }
}