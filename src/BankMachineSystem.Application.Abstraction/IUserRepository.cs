using BankMachineSystem.BankMachineSystem.Application.Contracts.Users;
using BankMachineSystem.BankMachineSystem.Application.Models;

namespace BankMachineSystem.BankMachineSystem.Application.Abstraction;

public interface IUserRepository
{
    Task<UserInfo?> FindUser(long userId);
    Task<bool> WithdrawMoney(long id, int amount);
    Task TopUpBalance(long id, int amount);

    Task<CreateAccountResult> CreateNewAccount(AccountInfo accountInfo);
}