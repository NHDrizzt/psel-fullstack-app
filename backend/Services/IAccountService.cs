using backend.DTO;
using backend.Models;

namespace backend.Services;

public interface IAccountService
{
    Task<List<Account>> GetAllAccounts();
    Task<Account> GetAccountById(int id);
    Task CreateAccount(Account account);
    Task UpdateAccount(int id, AccountDto accountDto);
    Task DeleteAccount(int id);
}