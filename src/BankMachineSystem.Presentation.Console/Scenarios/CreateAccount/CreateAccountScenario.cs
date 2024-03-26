using BankMachineSystem.BankMachineSystem.Application.Contracts.Admins;
using BankMachineSystem.BankMachineSystem.Application.Contracts.Users;
using BankMachineSystem.BankMachineSystem.Application.Models;
using Spectre.Console;

namespace BankMachineSystem.BankMachineSystem.Presentation.Console.Scenarios.CreateAccount;

public class CreateAccountScenario : IScenario
{
    private readonly IAdminService _adminService;

    public CreateAccountScenario(IAdminService adminService)
    {
        _adminService = adminService;
    }

    public string Name => "Create account";

    public async Task Run()
    {
        long id = AnsiConsole.Ask<long>("Enter id:");
        string password = AnsiConsole.Ask<string>("Enter password:");
        string name = AnsiConsole.Ask<string>("Enter name:");

        var result = await _adminService.CreateAccount(new AccountInfo(id, name, password));

        string message = result switch
        {
            CreateAccountResult.Success => "Successful login",
            CreateAccountResult.Failed => "Id already exists",
            _ => throw new ArgumentOutOfRangeException(nameof(result)),
        };

        AnsiConsole.WriteLine(message);
        AnsiConsole.Ask<string>("Ok");
    }
}