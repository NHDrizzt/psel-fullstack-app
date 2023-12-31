using backend.DTO;
using backend.Exceptions;
using backend.Models;
using backend.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository;

public class AccountRepository : IAccountRepository
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

    public async Task<bool> FindEmailIfExists(string email)
    {
        try
        {
            var account = await _context.Account.FirstOrDefaultAsync(x => x.Email == email);
            if (account == null)
            {
                return false;
            }
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<List<ErrorDetails>> ValidateAccount(Account account)
    {
        List<ErrorDetails> errors = new List<ErrorDetails>();
        var accountExists = await _context.Account
            .AnyAsync(a => a.Email == account.Email || a.CpfCnpj == account.CpfCnpj || a.Name == account.Name);

        if (accountExists)
        {
            if (await _context.Account.AnyAsync(a => a.Email == account.Email))
            {
                errors.Add(new ErrorDetails(
                    409,
                    "Conta já existe com esse email",
                    "EmailExists"
                ));
            }
        
            if (await _context.Account.AnyAsync(a => a.CpfCnpj == account.CpfCnpj))
            {
                errors.Add(new ErrorDetails(
                    409,
                    "Conta já existe com esse CPF/CNPJ",
                    "CpfCnpjExists"
                ));
            }

            if (await _context.Account.AnyAsync(a => a.Name == account.Name))
            {
                errors.Add(new ErrorDetails(
                    409,
                    "Conta já existe com esse nome",
                    "NameExists"
                ));
            }
        }

        return errors;
    }
     
    public async Task UpdateAccount(int id, AccountDto account)
    {
        try
        {
            var existingAccount = await _context.Account.FindAsync(id);
            if (existingAccount == null)
            {
                throw new KeyNotFoundException($"Conta com id {id} não foi encontrada.");
            }
            
            _context.Entry(existingAccount).CurrentValues.SetValues(account);
            await _context.SaveChangesAsync();
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogError(ex, "Error occurred in UpdateAccount");
            throw new KeyNotFoundException($"Não foi possível encontrar a conta com id {id}.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred in UpdateAccount");
            throw new Exception("Não foi possível atualizar a conta.");
        }
    }

    public async Task DeleteAccount(int id)
    {
        try
        {
            var account = await _context.Account.FirstOrDefaultAsync(x => x.Id == id);
            if (account == null)
            {
                throw new KeyNotFoundException($"Conta com ID {id} não foi encontrada.");
            }

            if (!account.AccountStatus)
            {
                throw new Exception($"Conta com ID {id} já está desativada.");
            }
            account.AccountStatus = false;
            _context.Account.Update(account);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred in DeleteAccount");
            throw;
        }
    }
}