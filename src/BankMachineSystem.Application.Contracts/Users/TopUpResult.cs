namespace BankMachineSystem.BankMachineSystem.Application.Contracts.Users;

public abstract record TopUpResult
{
    private TopUpResult() { }

    public sealed record Success : TopUpResult;

    public record Failed : TopUpResult
    {
        public record AccountNotFound : Failed;
    };
}