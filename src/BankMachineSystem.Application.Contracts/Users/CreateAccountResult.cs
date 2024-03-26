namespace BankMachineSystem.BankMachineSystem.Application.Contracts.Users;

public abstract record CreateAccountResult
{
    private CreateAccountResult() { }

    public sealed record Success : CreateAccountResult;

    public record Failed : CreateAccountResult
    {
        public sealed record IdExists : Failed;
    }
}