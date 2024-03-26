using BankMachineSystem.BankMachineSystem.Application.Models;
using BankMachineSystem.BankMachineSystem.Application.Models.TransactionsWithNames;

namespace BankMachineSystem.BankMachineSystem.Application.Contracts.Users;

public interface IUserService
{
    Task<LoginResult> Login(long id, string password);
    Task<UserInfo> GetAccountInfo();

    Task<TransactionsWithNamesHistory> GetTransactionHistory(int numberTransactions);

    Task<TopUpResult> TopUpBalance(long id, int amount);

    Task<WithdrawMoneyResult> WithdrawMoney(int amount);
}