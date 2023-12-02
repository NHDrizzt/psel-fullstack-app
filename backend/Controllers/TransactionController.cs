using backend.DTO;
using backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace backend.Controllers;


[ApiController]
[Route("api/transaction")]
public class TransactionController : ControllerBase
{
    
    private readonly ITransactionService _transactionService;

    public TransactionController(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }
    
    [HttpPost("payment")]
    [Authorize]
    public async Task<ActionResult> RegisterPayment(RegisterPaymentDto registerPaymentDto)
    {
        
        var idTransaction = await _transactionService.RegisterPayment(registerPaymentDto);
        
        return Ok(new { transactionId = idTransaction });
    }
    
    [HttpPost("cashback")]
    [Authorize]
    public async Task<ActionResult> RegisterCashback(RegisterCashbackDto registerCashbackDto)
    {
        await _transactionService.RegisterCashback(registerCashbackDto);
        return StatusCode(201);
    }
    
}