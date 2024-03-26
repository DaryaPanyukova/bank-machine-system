using BankMachineSystem.BankMachineSystem.Application.Models;

namespace BankMachineSystem.BankMachineSystem.Application.Contracts;

public interface ICurrentAccountService
{
    AccountInfo? AccountInfo { get; }
    Role? Role { get; }
}