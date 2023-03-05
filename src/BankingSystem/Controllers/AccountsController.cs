using BankingSystem.Models;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using BankingSystem.Services.Interface;

namespace BankingSystem.Controllers
{

    [ApiController]
  [Route("")]
  public class AccountsController : Controller
  {

    private IMapper _mapper;
    private IUserService _userService;

    public AccountsController(IMapper mapper, IUserService userService)
    {
      _mapper = mapper;
      _userService = userService;
    }

    [HttpPost]
    [Route("create_new_account")]
    public IActionResult CreateAccount([FromBody] CreateAccountModel newAccount)
    {
      if (!ModelState.IsValid) return BadRequest(newAccount);
      var account = _mapper.Map<Account>(newAccount);
      var response = _userService.Create(account, newAccount.Pin, newAccount.ConfirmPin);
      return Ok(response);
    }

    [HttpPost]
    [Route("delete_account/{id}")]
    public IActionResult DeleteAccount([FromRoute] int id)
    {
      _userService.Delete(id);
      return Ok();
    }
  }
}
