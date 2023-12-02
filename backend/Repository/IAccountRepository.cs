using backend.DTO;
using backend.Models;

namespace backend.Repository;

public interface IAccountRepository
{
    Task<List<Account>> GetAllAccounts();
    Task<Account> GetAccountById(int id);
    Task CreateAccount(Account account);
    Task UpdateAccount(int id, AccountDto accountDto);
    Task DeleteAccount(int id);
}