using backend.Models;

namespace backend.Repository;

public interface ICashbackRepository
{
    Task RegisterCashback(Cashback cashback);
}