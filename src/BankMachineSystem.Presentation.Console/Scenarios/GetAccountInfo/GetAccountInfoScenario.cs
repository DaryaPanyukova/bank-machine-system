using BankMachineSystem.BankMachineSystem.Application.Contracts.Users;
using BankMachineSystem.BankMachineSystem.Application.Models;
using Spectre.Console;

namespace BankMachineSystem.BankMachineSystem.Presentation.Console.Scenarios.GetAccountInfo;

public class GetAccountInfoScenario : IScenario
{
    private readonly IUserService _userService;

    public GetAccountInfoScenario(IUserService userService)
    {
        _userService = userService;
    }

    public string Name => "Get account info";

    public async Task Run()
    {
        UserInfo result = await _userService.GetAccountInfo();

        string message = $"Name: {result.Account.Name}\n" +
                         $"Bank account number: {result.Account.Id}" +
                         $"\nBalance: {result.Balance}";

        AnsiConsole.WriteLine(message);
        AnsiConsole.Ask<string>("Ok");
    }
}