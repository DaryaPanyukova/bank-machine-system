using System.Diagnostics.CodeAnalysis;
using BankMachineSystem.BankMachineSystem.Application.Contracts.Users;
using BankMachineSystem.BankMachineSystem.Application.Contracts;

namespace BankMachineSystem.BankMachineSystem.Presentation.Console.Scenarios.TopUpBalance;

public class TopUpBalanceScenarioProvider : IScenarioProvider
{
    private readonly IUserService _service;
    private readonly ICurrentAccountService? _currentUser;

    public TopUpBalanceScenarioProvider(
        IUserService service,
        ICurrentAccountService currentUser)
    {
        _service = service;
        _currentUser = currentUser;
    }

    public bool TryGetScenario(
        [NotNullWhen(true)] out IScenario? scenario)
    {
        scenario = new TopUpBalanceScenario(_service);
        return true;
    }
}