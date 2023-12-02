using backend.DTO;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository;

public class TransactionRepository : ITransactionRepository
{
    
    private readonly FinancialDbContext _context;
    
    private readonly ILogger<AccountRepository> _logger;
    
    public TransactionRepository(FinancialDbContext context, ILogger<AccountRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    
    public async Task<int> RegisterPayment(Transactions transactions)
    {
        try
        {
            _context.Transactions.Add(transactions);
            await _context.SaveChangesAsync();

            return transactions.IdTransaction;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred in RegisterPayment");
            throw;
        }
    }
    
    public async Task<Transactions> GetTransactionById(int id)
    {
        try
        {
            var transaction = await _context.Transactions.FirstOrDefaultAsync(x => x.IdTransaction == id);
            if (transaction == null)
            {
                throw new KeyNotFoundException($"Transação com ID {id} não foi encontrada.");
            }
            return transaction;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred in GetTransactionById");
            throw;
        }
    }
    
    public async Task<List<Transactions>> GetTransactions()
    {
        try
        {
            var transactions = await _context.Transactions.ToListAsync();
            return transactions;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred in GetTransactions");
            throw;
        }
    }
}