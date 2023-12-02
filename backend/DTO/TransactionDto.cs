namespace backend.DTO;

public class TransactionDto
{
    public int IdTransaction { get; set; }

    public int IdAccount { get; set; }

    public DateTime TransactionData { get; set; }

    public decimal Value { get; set; }

    public decimal? Cashback { get; set; }
}