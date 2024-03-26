namespace BankMachineSystem.BankMachineSystem.Presentation.Console;

public interface IScenario
{
    string Name { get; }
    Task Run();
}