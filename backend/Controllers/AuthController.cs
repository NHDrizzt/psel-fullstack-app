using backend.Models;
using backend.Repository;
using backend.Services;
using backend.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;


[ApiController]
[Route("api/auth")]
public class AuthController : Controller
{
    
    private readonly IAccountRepository _repository;
    
    public AuthController(IAccountRepository repository)
    {
        _repository = repository;
    }

    
    [HttpPost]
    public async Task<ActionResult<AccountViewModel>> Authenticate([FromBody] LoginModel loginModel)
    {
        AccountViewModel accountViewModel = new AccountViewModel();
        try
        {
            accountViewModel.Account = await _repository.FindUserByEmailAndPassword(loginModel);
            
            if (accountViewModel.Account == null)
            {
                return NotFound("Usuário não encontrado.");
            }
            
            if (!accountViewModel.Account.AccountStatus)
            {
                return BadRequest("Conta desativada.");
            }

            if (accountViewModel.Account.Role != Role.Admin)
            {
                return BadRequest("Conta não autorizada");
            }

            accountViewModel.Token = new TokenGenerator().Generate(accountViewModel.Account);
            accountViewModel.Account.Password = string.Empty;

        } catch (Exception e)
        {
            return BadRequest(e.Message);
        }

        return accountViewModel;
    }
}