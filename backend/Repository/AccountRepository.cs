using backend.Models;
using backend.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository;

public class AccountRepository
{
    private readonly FinancialDbContext _context;
    
    private readonly ILogger<AccountRepository> _logger;
    
    public AccountRepository(FinancialDbContext context, ILogger<AccountRepository> logger)
    {
        _context = context;
        _logger = logger;
    }
    
    
    public async Task<List<Account>> GetAllAccounts()
    {
        try
        {
            return await _context.Account.ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred in getAllAccounts");
            throw;
        }
    }

    public async Task<Account> GetAccountById(int id)
    {
        try
        {
            var account = await _context.Account.FirstOrDefaultAsync(x => x.Id == id);
            if (account == null)
            {
                throw new KeyNotFoundException($"Conta com ID {id} não foi encontrada.");
            }
            return account;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred in GetAccountById");
            throw;
        }
    }

    public async Task CreateAccount(Account account)
    {
        try
        {
            _context.Account.Add(account);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred in CreateAccount");
            throw;
        }
    }
    
    public async Task<Account> FindUserByEmailAndPassword (LoginModel loginModel)
    {
        try
        {
            var account = await _context.Account.FirstOrDefaultAsync(x => x.Email == loginModel.Email && x.Password == loginModel.Password);
            if (account == null)
            {
                throw new KeyNotFoundException($"Conta com email ou senha não foi encontrada.");
            }
            return account;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred in FindUserByEmailAndPassword");
            throw;
        }
    }
    
    public async Task UpdateAccount(Account account)
    {
        try
        {
            _context.Account.Update(account);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred in UpdateAccount");
            throw;
        }
    }

}