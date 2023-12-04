using backend.DTO;
using backend.Models;
using backend.ViewModel;

namespace backend.Repository;

public interface IAccountRepository
{
    Task<List<Account>> GetAllAccounts();
    Task<Account> GetAccountById(int id);
    Task CreateAccount(Account account);
    Task UpdateAccount(int id, AccountDto accountDto);
    Task DeleteAccount(int id);
    Task<Account> FindUserByEmailAndPassword(LoginModel loginModel);
    
    Task<bool> FindEmailIfExists(string email);
}