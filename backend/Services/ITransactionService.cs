using backend.DTO;

namespace backend.Services;

public interface ITransactionService
{
    Task<int> RegisterPayment(RegisterPaymentDto registerPaymentDto);
    Task RegisterCashback(RegisterCashbackDto registerCashbackDto);
}
