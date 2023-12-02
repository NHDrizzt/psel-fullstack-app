using backend.DTO;
using backend.Models;

namespace backend.Services;

public interface ITransactionService
{
    Task<int> RegisterPayment(RegisterPaymentDto registerPaymentDto);
    Task RegisterCashback(RegisterCashbackDto registerCashbackDto);
    Task<List<TransactionDto>> GetTransactions(int accountId);
}
