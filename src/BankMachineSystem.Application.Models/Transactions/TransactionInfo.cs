namespace BankMachineSystem.BankMachineSystem.Application.Models.Transactions;

public record TransactionInfo(string TransactionId, long? FromId, long? ToId, long Amount, string Date);