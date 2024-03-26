using BankMachineSystem.BankMachineSystem.Presentation.Console;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;

var collection = new ServiceCollection();

collection.AddApplication().AddInfrastructureDataAccess(configuration =>
    {
        configuration.Host = "localhost";
        configuration.Port = 5432;
        configuration.Username = "postgres";
        configuration.Password = "postgres";
        configuration.Database = "postgres";
        configuration.SslMode = "Prefer";
    })
    .AddPresentationConsole();

var provider = collection.BuildServiceProvider();
using var scope = provider.CreateScope();

scope.UseInfrastructureDataAccess();

var scenarioRunner = scope.ServiceProvider
    .GetRequiredService<ScenarioRunner>();

while (true)
{
    scenarioRunner.Run();
    AnsiConsole.Clear();
}