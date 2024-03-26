using BankMachineSystem.BankMachineSystem.Application.Models;

namespace BankMachineSystem.BankMachineSystem.Application.Abstraction;

public interface IAdminRepository
{
    Task<AccountInfo?> FindAdmin(long id);
}