using BankMachineSystem.BankMachineSystem.Application.Contracts.Users;
using BankMachineSystem.BankMachineSystem.Application.Models.TransactionsWithNames;
using Spectre.Console;

namespace BankMachineSystem.BankMachineSystem.Presentation.Console.Scenarios.GetTransactionHistory;

public class GetTransactionHistoryScenario : IScenario
{
    private readonly IUserService _userService;

    public GetTransactionHistoryScenario(IUserService userService)
    {
        _userService = userService;
    }

    public string Name => "Get transaction history";

    public async Task Run()
    {
        int numberTransactions = AnsiConsole.Ask<int>("How many last transactions to show?");
        TransactionsWithNamesHistory history = await _userService.GetTransactionHistory(numberTransactions);

        AnsiConsole.WriteLine($"Here are your last {numberTransactions} transactions: \n");
        foreach (TransactionWithNames transaction in history.Transactions)
        {
            AnsiConsole.WriteLine(transaction.AsString());
        }

        AnsiConsole.Ask<string>("Ok");
    }
}