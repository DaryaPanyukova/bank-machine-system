namespace BankMachineSystem.BankMachineSystem.Application.Contracts;

public abstract record LoginResult
{
    private LoginResult() { }

    public sealed record Success : LoginResult;

    public record Failed : LoginResult
    {
        public sealed record WrongPassword : Failed;

        public sealed record AccountNotFound : Failed;
    }
}