using backend.DTO;
using backend.Models;
using backend.Repository;
using backend.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;

[ApiController]
[Route("api/account")]
public class AccountController : Controller
{

    private readonly IAccountService _accountService;
    
    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<Account>>> GetAllAccounts()
    {
        var account = await _accountService.GetAllAccounts();

        return Ok(account);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Account>> GetAccountById(int id)
    { 
        var account = await _accountService.GetAccountById(id);
        return Ok(account);
    }
    
    
    [HttpPost]
    public async Task<ActionResult<Account>> CreateAccount([FromBody] Account account)
    {
        await _accountService.CreateAccount(account);
        return CreatedAtAction(nameof(GetAllAccounts), new { id = account.Id }, account);
    }

    [HttpPut ("{id}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Authorize(Policy = "RolePolicy")]
    public async Task<ActionResult> UpdateAccount(int id, [FromBody] AccountDto accountDto)
    {
        await _accountService.UpdateAccount(id, accountDto);
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Authorize]
    public async Task<ActionResult> DeleteAccount(int id)
    {
        await _accountService.DeleteAccount(id);
        return NoContent();
    }
    
    
}