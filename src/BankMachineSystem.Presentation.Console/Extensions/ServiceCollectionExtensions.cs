using BankMachineSystem.BankMachineSystem.Presentation.Console.Scenarios.GetAccountInfo;
using BankMachineSystem.BankMachineSystem.Presentation.Console.Scenarios.GetTransactionHistory;
using BankMachineSystem.BankMachineSystem.Presentation.Console.Scenarios.Login.LoginAdmin;
using BankMachineSystem.BankMachineSystem.Presentation.Console.Scenarios.Login.LoginUser;
using BankMachineSystem.BankMachineSystem.Presentation.Console.Scenarios.TopUpBalance;
using BankMachineSystem.BankMachineSystem.Presentation.Console.Scenarios.WithdrawMoney;
using Microsoft.Extensions.DependencyInjection;

namespace BankMachineSystem.BankMachineSystem.Presentation.Console.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPresentationConsole(this IServiceCollection collection)
    {
        collection.AddScoped<ScenarioRunner>();

        collection.AddScoped<IScenarioProvider, LoginUserScenarioProvider>();
        collection.AddScoped<IScenarioProvider, LoginAdminScenarioProvider>();
        collection.AddScoped<IScenarioProvider, GetAccountInfoScenarioProvider>();
        collection.AddScoped<IScenarioProvider, GetTransactionHistoryScenarioProvider>();
        collection.AddScoped<IScenarioProvider, TopUpBalanceScenarioProvider>();
        collection.AddScoped<IScenarioProvider, WithdrawMoneyScenarioProvider>();

        return collection;
    }
}