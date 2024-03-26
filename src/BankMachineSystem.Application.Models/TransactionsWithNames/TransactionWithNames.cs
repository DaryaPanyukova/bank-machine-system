using BankMachineSystem.BankMachineSystem.Application.Models.Transactions;

namespace BankMachineSystem.BankMachineSystem.Application.Models.TransactionsWithNames;

public record TransactionWithNames(TransactionInfo Transaction, string? NameFrom, string? NameTo)
{
    public string AsString()
    {
        string transactionType;
        if (NameFrom is null)
        {
            return $"Type: top up \n" +
                   $"Amount: {Transaction.Amount}";
        }

        if (NameTo is null)
        {
            return $"Type: withdraw \n" +
                   $"Amount: {Transaction.Amount}";
        }

        return $"Type: transfer \n " +
               $"From: {NameFrom} \t {Transaction.FromId} \n" +
               $"From: {NameTo} \t {Transaction.ToId} \n" +
               $"Amount: {Transaction.Amount}";
    }
}