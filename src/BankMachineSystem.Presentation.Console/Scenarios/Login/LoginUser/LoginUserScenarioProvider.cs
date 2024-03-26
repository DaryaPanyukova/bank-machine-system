using System.Diagnostics.CodeAnalysis;
using BankMachineSystem.BankMachineSystem.Application.Contracts.Users;
using BankMachineSystem.BankMachineSystem.Application.Contracts;

namespace BankMachineSystem.BankMachineSystem.Presentation.Console.Scenarios.Login.LoginUser;

public class LoginUserScenarioProvider : IScenarioProvider
{
    private readonly IUserService _service;
    private readonly ICurrentAccountService _currentAccountInfo;

    public LoginUserScenarioProvider(
        IUserService service,
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

        scenario = new LoginUserScenario(_service);
        return true;
    }
}