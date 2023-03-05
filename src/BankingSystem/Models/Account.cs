using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankingSystem.Models
{
  [Table("Accounts")]
  public class Account
  {
    [Key]
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string AccountName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public decimal CurrentAccountBalance { get; set; }
    public AccountType AccountType { get; set; }
    public string AccountNumberGenerated { get; set; }

    Random rand = new Random();

    public Account()
    {
      AccountNumberGenerated = Convert.ToString((long)rand.NextDouble() * 900000L + 100000L);
      AccountName = $"{FirstName}{LastName}";
    }
  }

  public enum AccountType
  {
    Savings,
    Current
  }
}
