using backend.DTO;
using backend.Models;

namespace backend.Repository;

public interface ITransactionRepository
{
    Task<int> RegisterPayment(Transactions transactions);
    
    Task<Transactions> GetTransactionById(int id);
}