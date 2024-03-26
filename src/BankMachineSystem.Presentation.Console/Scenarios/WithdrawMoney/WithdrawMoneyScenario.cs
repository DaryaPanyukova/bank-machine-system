using BankMachineSystem.BankMachineSystem.Application.Contracts.Users;
using Spectre.Console;

namespace BankMachineSystem.BankMachineSystem.Presentation.Console.Scenarios.WithdrawMoney;

public class WithdrawMoneyScenario : IScenario
{
    private readonly IUserService _userService;

    public WithdrawMoneyScenario(IUserService userService)
    {
        _userService = userService;
    }

    public string Name => "Withdraw money";

    public async Task Run()
    {
        int money = AnsiConsole.Ask<int>("What amount would you like to withdraw from your account?");

        WithdrawMoneyResult result = await _userService.WithdrawMoney(money);

        string message = result switch
        {
            WithdrawMoneyResult.Success => "Succeed",
            WithdrawMoneyResult.Failed.InsufficientFunds => "Insufficient funds for the transfer",
            _ => throw new ArgumentOutOfRangeException(nameof(result)),
        };

        AnsiConsole.WriteLine(message);
        AnsiConsole.Ask<string>("Ok");
    }
}