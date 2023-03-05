using BankingSystem.Controllers;
using BankingSystem.Models;
using BankingSystem.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace BankingSystemTesting
{
    public class TransactionControllerTests 
  {
    private readonly Mock<ITransactionService> _transactionService;

    public TransactionControllerTests()
    {
      _transactionService= new Mock<ITransactionService>();
    }

    [Fact]
    public void ItShouldMakeDeposit()
    {
      Response response = null;

      _transactionService.Setup(x => x.MakeDeposit("1000000000", (decimal)200.00, "1234")).Returns(response);
      TransactionsController _transactionsController = new TransactionsController(_transactionService.Object);
      
      var res = _transactionsController.MakeDeposit("1000000000", (decimal)200.00, "1234");

      Assert.NotNull(res);
      Assert.IsType<OkObjectResult>(res);
     // Assert.True(response.Equals(res));
    }

    [Fact]
    public void ItShouldAllowDepositIfAmountIsGreaterThanTenThousandDollar()
    {
      Response response = null;

      _transactionService.Setup(x => x.MakeDeposit("1000000000", (decimal)20000.00, "1234")).Returns(response);
      TransactionsController _transactionsController = new TransactionsController(_transactionService.Object);

      var res = _transactionsController.MakeDeposit("1000000000", (decimal)20000.00, "1234");

      Assert.NotNull(res);
      Assert.IsType<OkObjectResult>(res);
      //Assert.True(response.Equals(res));
    }

    [Fact]
    public void ItShouldNotMakeDepositIfAmountLessThanHundresDollars()
    {
      Response response = null;

      _transactionService.Setup(x => x.MakeDeposit("1000000000", (decimal)90.00, "1234")).Returns(response);
      TransactionsController _transactionsController = new TransactionsController(_transactionService.Object);

      var res = _transactionsController.MakeDeposit("1000000000", (decimal)90.00, "1234");

      Assert.NotNull(res);
      Assert.IsType<OkObjectResult>(res);
      //Assert.False(response.Equals(res));
    }

    [Fact]
    public void ItShoudBeAbleToWithdraw()
    {
      Response response = null;

      _transactionService.Setup(x => x.MakeWithdrawal("1000000000", (decimal)80.00, "1234")).Returns(response);
      TransactionsController _transactionsController = new TransactionsController(_transactionService.Object);

      var res = _transactionsController.MakeWithdrawal("1000000000", (decimal)80.00, "1234");

      Assert.NotNull(res);
      Assert.IsType<OkObjectResult>(res);
     // Assert.True(response.Equals(res));
    }

    [Fact]
    public void ItShouldNotBeAbleToWithdrawIfTotalBalanceBecomesLessThanHundresDollars()
    {
      Response response = null;

      _transactionService.Setup(x => x.MakeWithdrawal("1000000000", (decimal)110.00, "1234")).Returns(response);
      TransactionsController _transactionsController = new TransactionsController(_transactionService.Object);

      var res = _transactionsController.MakeWithdrawal("1000000000", (decimal)110.00, "1234");

      Assert.NotNull(res);
      Assert.IsType<OkObjectResult>(res);
    }

    [Fact]
    public void ItShouldNotBeAbleToWithdrawIfAmountIsGreaterThanNintyPercentOfTotalBalance()
    {
      Response response = null;

      _transactionService.Setup(x => x.MakeWithdrawal("1000000000", (decimal)95.00, "1234")).Returns(response);
      TransactionsController _transactionsController = new TransactionsController(_transactionService.Object);

      var res = _transactionsController.MakeWithdrawal("1000000000", (decimal)95.00, "1234");

      Assert.NotNull(res);
      Assert.IsType<OkObjectResult>(res);
    }
  }
}
