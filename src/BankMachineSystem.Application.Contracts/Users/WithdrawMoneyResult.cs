namespace BankMachineSystem.BankMachineSystem.Application.Contracts.Users;

public abstract record WithdrawMoneyResult
{
    public sealed record Success : WithdrawMoneyResult;

    public record Failed : WithdrawMoneyResult
    {
        public sealed record InsufficientFunds : Failed;
    }
}