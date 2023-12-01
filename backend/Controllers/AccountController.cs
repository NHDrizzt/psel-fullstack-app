using backend.Models;
using backend.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : Controller
{
    
    private readonly AccountRepository _repository;
    
    public AccountController(AccountRepository repository)
    {
        _repository = repository;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<Account>>> GetAllAccounts()
    {
        var account = await _repository.GetAllAccounts();

        return Ok(account);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Account>> GetAccountById(int id)
    { 
        var account = await _repository.GetAccountById(id);
        return Ok(account);
    }
    
    
    [HttpPost]
    public async Task<ActionResult<Account>> CreateAccount(Account account)
    {
        await _repository.CreateAccount(account);
        return CreatedAtAction(nameof(GetAllAccounts), new { id = account.Id }, account);
    }

    [HttpPut]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Authorize(Policy = "RolePolicy")]
    public async Task<ActionResult> UpdateAccount(Account account)
    {
        await _repository.UpdateAccount(account);
        return NoContent();
    }
    
    
}