using BankingSystem.Models;
using System;

namespace BankingSystem.Services.Interface
{
    public interface ITransactionService
    {
        Response MakeDeposit(string AccountNumber, decimal Amount, string TransactionPin);
        Response MakeWithdrawal(string AccountNumber, decimal Amount, string TransactionPin);
    }
}
