using System.Diagnostics.CodeAnalysis;
using BankMachineSystem.BankMachineSystem.Application.Contracts.Admins;
using BankMachineSystem.BankMachineSystem.Application.Contracts;

namespace BankMachineSystem.BankMachineSystem.Presentation.Console.Scenarios.Login.LoginAdmin;

public class LoginAdminScenarioProvider : IScenarioProvider
{
    private readonly IAdminService _service;
    private readonly ICurrentAccountService _currentAccountInfo;

    public LoginAdminScenarioProvider(
        IAdminService service,
        ICurrentAccountService currentAccountInfo)
    {
        _service = service;
        _currentAccountInfo = currentAccountInfo;
    }

    public bool TryGetScenario(
        [NotNullWhen(true)] out IScenario? scenario)
    {
        if (_currentAccountInfo.AccountInfo is not null)
        {
            scenario = null;
            return false;
        }

        scenario = new LoginAdminScenario(_service);
        return true;
    }
}