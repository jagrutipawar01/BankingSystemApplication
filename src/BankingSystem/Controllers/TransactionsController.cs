using AutoMapper;
using BankingSystem.Models;
using BankingSystem.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace BankingSystem.Controllers
{
    [ApiController]
  [Route("transaction/")]
  public class TransactionsController : Controller
  {
    private readonly ITransactionService _transactionService;

    public TransactionsController(ITransactionService transactionService)
    {
      _transactionService = transactionService;
    }


    [HttpPost]
    [Route("make_deposit/{accountnumber}/{amount}/{TransactionPin}")]
    public IActionResult MakeDeposit([FromRoute]string AccountNumber, [FromRoute]decimal Amount, [FromRoute]string TransactionPin)
    {
      return Ok(_transactionService.MakeDeposit(AccountNumber, Amount, TransactionPin));
    }
    

    [HttpPost]
    [Route("make_withdrawal/{accountnumber}/{amount}/{TransactionPin}")]
    public IActionResult MakeWithdrawal([FromRoute]string AccountNumber, [FromRoute]decimal Amount, [FromRoute]string TransactionPin)
    {
      return Ok(_transactionService.MakeWithdrawal(AccountNumber, Amount, TransactionPin));
    }
  }
}
