using System.Diagnostics.CodeAnalysis;
using BankMachineSystem.BankMachineSystem.Application.Contracts.Admins;
using BankMachineSystem.BankMachineSystem.Application.Models;
using BankMachineSystem.BankMachineSystem.Application.Contracts;

namespace BankMachineSystem.BankMachineSystem.Presentation.Console.Scenarios.CreateAccount;

public class CreateAccountScenarioProvider : IScenarioProvider
{
    private readonly IAdminService _service;
    private readonly ICurrentAccountService _currentAdmin;

    public CreateAccountScenarioProvider(
        IAdminService service,
        ICurrentAccountService currentAdmin)
    {
        _service = service;
        _currentAdmin = currentAdmin;
    }

    public bool TryGetScenario(
        [NotNullWhen(true)] out IScenario? scenario)
    {
        if (_currentAdmin.AccountInfo is null || _currentAdmin.Role != Role.Admin)
        {
            scenario = null;
            return false;
        }

        scenario = new CreateAccountScenario(_service);
        return true;
    }
}