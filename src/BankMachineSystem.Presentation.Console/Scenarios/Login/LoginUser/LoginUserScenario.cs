using BankMachineSystem.BankMachineSystem.Application.Contracts.Users;
using BankMachineSystem.BankMachineSystem.Application.Contracts;
using Spectre.Console;

namespace BankMachineSystem.BankMachineSystem.Presentation.Console.Scenarios.Login.LoginUser;

public class LoginUserScenario : IScenario
{
    private readonly IUserService _userService;
    public LoginUserScenario(IUserService userService)
    {
        _userService = userService;
    }

    public string Name => "Login as user";

    public async Task Run()
    {
        int bankAccountNumber = AnsiConsole.Ask<int>("Enter bank account number");
        string password = AnsiConsole.Ask<string>("Enter password");

        LoginResult result = await _userService.Login(bankAccountNumber, password);

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