using BankMachineSystem.BankMachineSystem.Application.Contracts.Users;
using Spectre.Console;

namespace BankMachineSystem.BankMachineSystem.Presentation.Console.Scenarios.TopUpBalance;

public class TopUpBalanceScenario : IScenario
{
    private readonly IUserService _userService;

    public TopUpBalanceScenario(IUserService userService)
    {
        _userService = userService;
    }

    public string Name => "Top up balance";

    public async Task Run()
    {
        int bankAccountNumber = AnsiConsole.Ask<int>("Enter bank account number: ");
        int amount = AnsiConsole.Ask<int>("How much amount do you want to deposit into account?");
        var result =  await _userService.TopUpBalance(bankAccountNumber, amount);
      
        string message = result switch
        {
            TopUpResult.Success => "Succeed",
            TopUpResult.Failed.AccountNotFound => "Account not found",
            _ => throw new ArgumentOutOfRangeException(nameof(result)),
        };
        AnsiConsole.WriteLine(message);
        AnsiConsole.Ask<string>("Ok");
    }
}