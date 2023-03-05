using BankingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace BankingSystem.DAL
{
  public class BankDbContext : DbContext
  {
    public BankDbContext(DbContextOptions<BankDbContext> options) : base(options)
    {
    }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
  }
}
