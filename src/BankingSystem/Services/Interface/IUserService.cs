using BankingSystem.Models;
using System.Collections.Generic;

namespace BankingSystem.Services.Interface
{
    public interface IUserService
    {
        Account Create(Account account, string Pin, string ConfirmPin);
        void Delete(int Id);
        Account GetByAccountNumber(string AccountNumber);
    }
}
