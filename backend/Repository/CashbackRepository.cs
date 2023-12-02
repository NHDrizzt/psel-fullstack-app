using backend.Models;

namespace backend.Repository;

public class CashbackRepository : ICashbackRepository
{
    private readonly FinancialDbContext _context;
    
    private readonly ILogger<CashbackRepository> _logger;
    
    public CashbackRepository(FinancialDbContext context, ILogger<CashbackRepository> logger)
    {
        _context = context;
        _logger = logger;
    }
    
    public async Task RegisterCashback(Cashback cashback)
    {
        try
        {
            _context.Cashbacks.Add(cashback);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred in RegisterCashback");
            throw;
        }
    }
}