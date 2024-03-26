using System.Diagnostics.CodeAnalysis;
using BankMachineSystem.BankMachineSystem.Application.Contracts.Users;
using BankMachineSystem.BankMachineSystem.Application.Models;
using BankMachineSystem.BankMachineSystem.Application.Contracts;

namespace BankMachineSystem.BankMachineSystem.Presentation.Console.Scenarios.GetTransactionHistory;

public class GetTransactionHistoryScenarioProvider : IScenarioProvider
{
    private readonly IUserService _service;
    private readonly ICurrentAccountService _currentUser;

    public GetTransactionHistoryScenarioProvider(
        IUserService service,
        ICurrentAccountService currentUser)
    {
        _service = service;
        _currentUser = currentUser;
    }

    public bool TryGetScenario(
        [NotNullWhen(true)] out IScenario? scenario)
    {
        if (_currentUser.AccountInfo is null || _currentUser.Role != Role.User)
        {
            scenario = null;
            return false;
        }

        scenario = new GetTransactionHistoryScenario(_service);
        return true;
    }
}