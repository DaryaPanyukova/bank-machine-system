

using BankMachineSystem.BankMachineSystem.Application;
using BankMachineSystem.BankMachineSystem.Application.Abstraction;
using BankMachineSystem.BankMachineSystem.Application.Contracts.Users;
using BankMachineSystem.BankMachineSystem.Application.Models;
using BankMachineSystem.BankMachineSystem.Application.Models.Transactions;
using BankMachineSystem.BankMachineSystem.Application.Users;
using Moq;
using Xunit;

namespace BankMachineSystem.Tests;

public class UserServiceTests
{
    [Fact]
    public async Task WithdrawMoney_WithSufficientBalance_ShouldUpdateBalanceAndReturnSuccess()
    {
        // Arrange
        long accountId = 1;
        int initialBalance = 100;
        int withdrawAmount = 517;
        var accountInfo = new AccountInfo(accountId, "name", "password");
        var user = new UserInfo(
            Account: accountInfo,
            Balance: initialBalance);
        var userRepositoryMock = new Mock<IUserRepository>();
        userRepositoryMock.Setup(repo => repo.WithdrawMoney(accountId, withdrawAmount)).ReturnsAsync(true);
        var transactionRepositoryMock = new Mock<ITransactionRepository>();

        var userService = new UserService(userRepositoryMock.Object, transactionRepositoryMock.Object,
            new CurrentAccountManager { AccountInfo = accountInfo });

        // Act
        var result = await userService.WithdrawMoney(withdrawAmount);

        // Assert
        Assert.IsType<WithdrawMoneyResult.Success>(result);
    }

    [Fact]
    public async Task WithdrawMoney_WithInsufficientBalance_ShouldReturnError()
    {
        long accountId = 1;
        int initialBalance = 100;
        int withdrawAmount = 50;
        var accountInfo = new AccountInfo(accountId, "name", "password");
        var user = new UserInfo(
            Account: accountInfo,
            Balance: initialBalance);
        var userRepositoryMock = new Mock<IUserRepository>();
        userRepositoryMock.Setup(repo => repo.WithdrawMoney(accountId, withdrawAmount)).ReturnsAsync(false);
        var transactionRepositoryMock = new Mock<ITransactionRepository>();

        var userService = new UserService(userRepositoryMock.Object, transactionRepositoryMock.Object,
            new CurrentAccountManager { AccountInfo = accountInfo });

        // Act
        var result = await userService.WithdrawMoney(withdrawAmount);

        // Assert
        Assert.IsType<WithdrawMoneyResult.Failed.InsufficientFunds>(result);
    }

    [Fact]
    public async Task TopUpBalance_ShouldUpdateBalanceAndReturnSuccess()
    {
        // Arrange
        var userRepositoryMock = new Mock<IUserRepository>();
        var transactionRepositoryMock = new Mock<ITransactionRepository>();
        var currentAccountManagerMock = new Mock<CurrentAccountManager>();

        var userService = new UserService(userRepositoryMock.Object, transactionRepositoryMock.Object,
            currentAccountManagerMock.Object);

        long accountId = 1;
        int initialBalance = 100;
        int topUpAmount = 50;

        var user = new UserInfo(
            Account: new AccountInfo(accountId, "name", "password"),
            Balance: initialBalance);
        userRepositoryMock.Setup(repo => repo.FindUser(accountId)).ReturnsAsync(user);

        // Act
        var result = await userService.TopUpBalance(accountId, topUpAmount);

        // Assert
        Assert.True(result is TopUpResult.Success);
        userRepositoryMock.Verify(repo => repo.TopUpBalance(accountId, topUpAmount), Times.Once);
        transactionRepositoryMock.Verify(repo => repo.RegisterNewTransaction(It.IsAny<TransactionInfo>()), Times.Once);
    }
}