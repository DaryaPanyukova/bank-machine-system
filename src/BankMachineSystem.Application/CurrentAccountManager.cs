using BankMachineSystem.BankMachineSystem.Application.Contracts;
using BankMachineSystem.BankMachineSystem.Application.Models;

namespace BankMachineSystem.BankMachineSystem.Application;

public class CurrentAccountManager : ICurrentAccountService
{
    public AccountInfo? AccountInfo { get; set; }
    public Role? Role { get; set; }
}