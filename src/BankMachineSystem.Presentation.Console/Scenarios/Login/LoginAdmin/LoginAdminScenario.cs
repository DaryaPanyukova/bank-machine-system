using BankMachineSystem.BankMachineSystem.Application.Contracts.Admins;
using BankMachineSystem.BankMachineSystem.Application.Contracts;
using Spectre.Console;

namespace BankMachineSystem.BankMachineSystem.Presentation.Console.Scenarios.Login.LoginAdmin;

public class LoginAdminScenario : IScenario
{
    private readonly IAdminService _adminService;

    public LoginAdminScenario(IAdminService adminService)
    {
        _adminService = adminService;
    }

    public string Name => "Login as admin";

    public async Task Run()
    {
        long id = AnsiConsole.Ask<long>("Enter id:");
        string password = AnsiConsole.Ask<string>("Enter password:");

        LoginResult result = await _adminService.Login(id, password);


        string message = result switch
        {
            LoginResult.Success => "Successful login",
            LoginResult.Failed.WrongPassword => "Wrong password",
            LoginResult.Failed.AccountNotFound => "Account not found",
            _ => throw new ArgumentOutOfRangeException(nameof(result)),
        };

        AnsiConsole.WriteLine(message);
        AnsiConsole.Ask<string>("Ok");
    }
}