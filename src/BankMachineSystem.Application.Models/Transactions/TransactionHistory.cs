namespace BankMachineSystem.BankMachineSystem.Application.Models.Transactions;

public record TransactionHistory
{
    public List<TransactionInfo> Transactions = new List<TransactionInfo>();
};