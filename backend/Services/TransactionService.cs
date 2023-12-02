using backend.DTO;
using backend.Models;
using backend.Repository;

namespace backend.Services;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly ICashbackRepository _cashbackRepository;
    private readonly IAccountRepository _accountRepository;
    
    public TransactionService(ITransactionRepository transactionRepository, ICashbackRepository cashbackRepository, IAccountRepository accountRepository)
    {
        _transactionRepository = transactionRepository;
        _cashbackRepository = cashbackRepository;
        _accountRepository = accountRepository;
    }
    
    public async Task<int> RegisterPayment(RegisterPaymentDto registerPaymentDto)
    {
        try
        {
            var account = await _accountRepository.GetAccountById(registerPaymentDto.IdAccount);
        
            if (account == null)
            {
                throw new KeyNotFoundException($"Conta com ID {registerPaymentDto.IdAccount} não foi encontrada.");
            }
            
            if(!account.AccountStatus )
            {
                throw new Exception($"Conta com ID {registerPaymentDto.IdAccount} está inativa.");
            }
        
            var transaction = new Transactions
            {
                IdAccount = registerPaymentDto.IdAccount,
                Value = registerPaymentDto.Value,
                TransactionData = DateTime.Now,
                Cashback = 0
            };
        
            return await _transactionRepository.RegisterPayment(transaction);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    public async Task RegisterCashback(RegisterCashbackDto registerCashbackDto)
    {
        try
        {
            var transaction = await _transactionRepository.GetTransactionById(registerCashbackDto.IdTransaction);
            var account = await _accountRepository.GetAccountById(transaction.IdAccount);
            
            if(!account.AccountStatus )
            {
                throw new Exception($"Conta com ID {transaction.IdAccount} está inativa.");
            }
            if (transaction == null)
            {
                throw new KeyNotFoundException($"Transação com ID {registerCashbackDto.IdTransaction} não foi encontrada.");
            }
        
            var cashback = new Cashback
            {
                IdTransaction = registerCashbackDto.IdTransaction,
                TaxCashback = registerCashbackDto.TaxCashback
            };
        
            var cashbackValue = transaction.Value * (cashback.TaxCashback / 100);
            transaction.Cashback = cashbackValue;
            
            await _cashbackRepository.RegisterCashback(cashback);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}