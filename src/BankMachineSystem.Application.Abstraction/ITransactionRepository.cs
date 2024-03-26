using BankMachineSystem.BankMachineSystem.Application.Models.Transactions;

namespace BankMachineSystem.BankMachineSystem.Application.Abstraction;

public interface ITransactionRepository
{
    Task<TransactionHistory> GetLastTransactions(long accountId, int numberTransactions);
    Task RegisterNewTransaction(TransactionInfo transaction);
}