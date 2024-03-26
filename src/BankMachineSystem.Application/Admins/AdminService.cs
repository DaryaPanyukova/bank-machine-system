using BankMachineSystem.BankMachineSystem.Application.Abstraction;
using BankMachineSystem.BankMachineSystem.Application.Contracts.Admins;
using BankMachineSystem.BankMachineSystem.Application.Contracts.Users;
using BankMachineSystem.BankMachineSystem.Application.Contracts;
using BankMachineSystem.BankMachineSystem.Application.Models;

namespace BankMachineSystem.BankMachineSystem.Application.Admins;


public class AdminService : IAdminService
{
    private readonly IUserRepository _userRepository;
    private readonly IAdminRepository _adminRepository;
    private readonly CurrentAccountManager _currentAccountInfo;

    public AdminService(IUserRepository userRepository, IAdminRepository adminRepository,
        CurrentAccountManager currentAccountInfo)
    {
        _userRepository = userRepository;
        _adminRepository = adminRepository;
        _currentAccountInfo = currentAccountInfo;
    }

    public async Task<LoginResult> Login(long id, string password)
    {
        var account = await _adminRepository.FindAdmin(id).ConfigureAwait(false);

        if (account is null)
        {
            return new LoginResult.Failed.AccountNotFound();
        }

        if (account.Password != password)
        {
            return new LoginResult.Failed.WrongPassword();
        }

        _currentAccountInfo.AccountInfo = account;
        _currentAccountInfo.Role = Role.Admin;
        return new LoginResult.Success();
    }

    public async Task<CreateAccountResult> CreateAccount(AccountInfo account)
    {
        return await _userRepository.CreateNewAccount(account);
    }
}