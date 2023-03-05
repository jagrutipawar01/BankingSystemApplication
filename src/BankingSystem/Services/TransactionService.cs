using BankingSystem.DAL;
using BankingSystem.Models;
using BankingSystem.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;

namespace BankingSystem.Services
{
    public class TransactionService : ITransactionService
  {

    private BankDbContext _dbContext;
    private IUserService _userService;

    public TransactionService(BankDbContext dbContext, IUserService userService)
    {
      _dbContext = dbContext;
      _userService = userService;
    }

    public Response MakeDeposit(string AccountNumber, decimal Amount, string TransactionPin)
    {
      Response response = new Response();
      Account account;
      Transaction transaction = new Transaction();

      try
      {
        account = _userService.GetByAccountNumber(AccountNumber);
        account.CurrentAccountBalance += Amount;

        if (account.CurrentAccountBalance < 100)
        {
          transaction.TransactionStatus = TranStatus.Error;
          response.ResponseCode = "00";
          response.ResponseMessage = "Cannot have less than 100 dollar balance";
          response.Data = null;
          return response;
        }

        if (Amount > 10000)
        {
          transaction.TransactionStatus = TranStatus.Error;
          response.ResponseCode = "00";
          response.ResponseMessage = "Amount cannot be more that 10000 dollar for single transaction";
          response.Data = null;
          return response;
        }

        if (_dbContext.Entry(account).State == EntityState.Modified)
        {
          transaction.TransactionStatus = TranStatus.Success;
          response.ResponseCode = "00";
          response.ResponseMessage = "Transaction Successful!";
          response.Data = null;

        }
        else
        {
          transaction.TransactionStatus = TranStatus.Failed;
          response.ResponseCode = "00";
          response.ResponseMessage = "Transaction Failed!";
          response.Data = null;
        }
      }
      catch (Exception ex)
      {

      }

      transaction.TransactionDate = DateTime.Now;
      transaction.TransactionType = TranType.Deposit;
      transaction.TransactionAmount = Amount;
      transaction.TransactionAccount = AccountNumber;
      transaction.TransactionParticulars = $"NEW Transaction FOR ACCOUNT {JsonConvert.SerializeObject(transaction.TransactionAccount)} ON {transaction.TransactionDate} TRAN_TYPE =>  {transaction.TransactionType} TRAN_STATUS => {transaction.TransactionStatus}";

      _dbContext.Transactions.Add(transaction);
      _dbContext.SaveChanges();

      return response;

    }

    public Response MakeWithdrawal(string AccountNumber, decimal Amount, string TransactionPin)
    {
      Response response = new Response();
      Account account;
      Transaction transaction = new Transaction();

      try
      {
        account = _userService.GetByAccountNumber(AccountNumber);
        account.CurrentAccountBalance -= Amount; 

        if(account.CurrentAccountBalance < 100)
        {
          transaction.TransactionStatus = TranStatus.Error;
          response.ResponseCode = "00";
          response.ResponseMessage = "Cannot have less than 100 dollar balance";
          response.Data = null;
          return response;
        }

        var nintyPercentAmount = (account.CurrentAccountBalance / 10) * 9;

        if(Amount > nintyPercentAmount)
        {
          transaction.TransactionStatus = TranStatus.Error;
          response.ResponseCode = "00";
          response.ResponseMessage = "Cannot Withdraw More Then 90% of total amount in single transaction";
          response.Data = null;
          return response;
        }

        if (_dbContext.Entry(account).State == EntityState.Modified)
        {
          transaction.TransactionStatus = TranStatus.Success;
          response.ResponseCode = "00";
          response.ResponseMessage = "Transaction Successful!";
          response.Data = null;

        }
        else
        {
          transaction.TransactionStatus = TranStatus.Failed;
          response.ResponseCode = "00";
          response.ResponseMessage = "Transaction Failed!";
          response.Data = null;
        }
      }
      catch (Exception ex)
      {

      }

      transaction.TransactionDate = DateTime.Now;
      transaction.TransactionType = TranType.Withdrawal;
      transaction.TransactionAmount = Amount;
      transaction.TransactionAccount = AccountNumber;
      transaction.TransactionParticulars = $"NEW Transaction FOR ACCOUNT {JsonConvert.SerializeObject(transaction.TransactionAccount)} ON {transaction.TransactionDate} TRAN_TYPE =>  {transaction.TransactionType} TRAN_STATUS => {transaction.TransactionStatus}";

      _dbContext.Transactions.Add(transaction);
      _dbContext.SaveChanges();


      return response;
    }
  }
}
