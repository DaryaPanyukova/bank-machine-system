namespace BankMachineSystem.BankMachineSystem.Application.Models.TransactionsWithNames;

public record TransactionsWithNamesHistory
{
    public List<TransactionWithNames> Transactions = new List<TransactionWithNames>();
}