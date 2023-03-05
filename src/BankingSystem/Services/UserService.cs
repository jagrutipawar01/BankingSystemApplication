using BankingSystem.DAL;
using BankingSystem.Models;
using BankingSystem.Services.Interface;
using System;
using System.Linq;

namespace BankingSystem.Services
{
    public class UserService : IUserService
  {

    private readonly BankDbContext _dbContext;
    public UserService(BankDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public Account Create(Account account, string Pin, string ConfirmPin)
    {
      if (string.IsNullOrWhiteSpace(Pin)) throw new ArgumentNullException("Pin cannot be empty");
      if (!Pin.Equals(ConfirmPin)) throw new ApplicationException("Pins do not match.");

      if (account.CurrentAccountBalance < 100)
      {
        throw new ApplicationException("Cannot have less than 100 dollar balance");
      }

      account.AccountNumberGenerated += 1;
      _dbContext.Accounts.Add(account);
      _dbContext.SaveChanges();

      return account;
    }

    public void Delete(int Id)
    {
      var account = _dbContext.Accounts.Find(Id);
      if (account != null)
      {
        _dbContext.Accounts.Remove(account);

        _dbContext.SaveChanges();
      }
    }

    public Account GetByAccountNumber(string AccountNumber)
    {
      var account = _dbContext.Accounts.Where(x => x.AccountNumberGenerated == AccountNumber).SingleOrDefault();
      if (account == null)
      {
        return null;
      }

      return account;
    }
  }
}
