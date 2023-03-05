using BankingSystem.Controllers;
using BankingSystem.Models;
using Moq;
using AutoMapper;
using Xunit;
using BankingSystem.Profiles;
using Microsoft.AspNetCore.Mvc;
using BankingSystem.Services.Interface;

namespace BankingSystemTesting
{
    public class AccountsControllerTests
  {

    private readonly Mock<IUserService> _userService;
    private readonly IMapper _mapper;
    private readonly AccountsController _accountsController;

    public AccountsControllerTests()
    {
      _userService = new Mock<IUserService>();
      var mappingConfig = new MapperConfiguration(mc =>
      {
        mc.AddProfile(new AutoMapperProfile());
      });
      IMapper mapper = mappingConfig.CreateMapper();
      _mapper = mapper;
      _accountsController = new AccountsController(_mapper,_userService.Object);
    }

    [Fact]
    public void ItShouldCreateNewAccount()
    {
      Account account = new Account
      {
        FirstName = "Jagruti",
        LastName = "Pawar",
        PhoneNumber = "1234567898",
        Email = "jagrutichavan@gmail.com",
        CurrentAccountBalance = 101
      };

      var accounts = new CreateAccountModel
      {
        FirstName = "Jagruti",
        LastName = "Pawar",
        PhoneNumber = "1234567898",
        Email = "jagrutichavan@gmail.com",
        CurrentAccountBalance = 101,
        Pin = "1234",
        ConfirmPin = "1234"
      };

      _userService.Setup(p => p.Create(account, "1234", "1234")).Returns(account);
      var response = _accountsController.CreateAccount(accounts);

      Assert.IsType<OkObjectResult>(response);
    }

    [Fact]
    public void ItShouldNotCreateNewAccountIfBalanceIsLessThanHundredDollar()
    {
      Account account = new Account
      {
        FirstName = "Jagruti",
        LastName = "Pawar",
        PhoneNumber = "1234567898",
        Email = "jagrutichavan@gmail.com",
        CurrentAccountBalance = 90
      };

      var accounts = new CreateAccountModel
      {
        FirstName = "Jagruti",
        LastName = "Pawar",
        PhoneNumber = "1234567898",
        Email = "jagrutichavan@gmail.com",
        CurrentAccountBalance = 90,
        Pin = "1234",
        ConfirmPin = "1234"
      };

      _userService.Setup(p => p.Create(account, "1234", "1234")).Returns(account);
      var response = _accountsController.CreateAccount(accounts);

      Assert.IsType<OkObjectResult>(response);
    }

    [Fact]
    public void ItShouldCreateAnotherAccountForSameUser()
    {
      Account account = new Account();

      CreateAccountModel createAccount = new CreateAccountModel();
      createAccount.FirstName = "Jagruti";
      createAccount.LastName = "Pawar";
      createAccount.PhoneNumber = "1234567898";
      createAccount.Email = "jagrutichavan@gmail.com";
      createAccount.CurrentAccountBalance = 101;
      createAccount.Pin = "1234";
      createAccount.ConfirmPin = "1234";

      var response = _accountsController.CreateAccount(createAccount);

      Assert.IsType<OkObjectResult>(response);
    }

    [Fact]
    public void ItShouldDeleteAccount()
    {
      Account account = new Account();

      account.Id = 1;

      var response = _accountsController.DeleteAccount(1);

      Assert.IsType<OkResult>(response);
    }
  }
}
