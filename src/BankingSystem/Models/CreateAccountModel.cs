using System.ComponentModel.DataAnnotations;
using System;

namespace BankingSystem.Models
{
  public class CreateAccountModel
  {
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public decimal CurrentAccountBalance { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateLastUpdated { get; set; }

    [Required]
    [RegularExpression(@"^[0-9]{4}$")]
    public string Pin { get; set; }
    [Required]
    [Compare("Pin", ErrorMessage = "Pins do not match")]
    public string ConfirmPin { get; set; }
  }
}
