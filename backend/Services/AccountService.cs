using backend.DTO;
using backend.Models;
using backend.Repository;

namespace backend.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;
    
    public AccountService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }
    
    public async Task<List<Account>> GetAllAccounts()
    {
        return await _accountRepository.GetAllAccounts();
    }
    
    public async Task<Account> GetAccountById(int id)
    {
        return await _accountRepository.GetAccountById(id);
    }
    
    public async Task CreateAccount(Account account)
    {
        await _accountRepository.CreateAccount(account);
    }
    
    public async Task UpdateAccount(int id, AccountDto accountDto)
    {
        await _accountRepository.UpdateAccount(id, accountDto);
    }
    
    public async Task DeleteAccount(int id)
    {
        await _accountRepository.DeleteAccount(id);
    }
}