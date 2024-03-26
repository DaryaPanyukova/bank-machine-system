using System.Diagnostics.CodeAnalysis;

namespace BankMachineSystem.BankMachineSystem.Presentation.Console;

public interface IScenarioProvider
{
    bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario);
}