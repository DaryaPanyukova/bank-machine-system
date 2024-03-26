using BankMachineSystem.BankMachineSystem.Application.Contracts.Users;
using BankMachineSystem.BankMachineSystem.Application.Models;

namespace BankMachineSystem.BankMachineSystem.Application.Contracts.Admins;
public interface IAdminService
{
    Task<LoginResult> Login(long id, string password);
    Task<CreateAccountResult> CreateAccount(AccountInfo account);
}