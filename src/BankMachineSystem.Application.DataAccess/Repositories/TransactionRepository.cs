using System.Globalization;
using BankMachineSystem.BankMachineSystem.Application.Abstraction;
using BankMachineSystem.BankMachineSystem.Application.Models.Transactions;
using Itmo.Dev.Platform.Postgres.Connection;
using Itmo.Dev.Platform.Postgres.Extensions;
using Npgsql;

namespace BankMachineSystem.BankMachineSystem.Application.DataAccess.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly IPostgresConnectionProvider _connectionProvider;

    public TransactionRepository(IPostgresConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }

    public async Task<TransactionHistory> GetLastTransactions(long accountId, int numberTransactions)
    {
        const string sql = @"
            SELECT TOP(:numberTransactions) TransactionId, FromId, ToId, Amount, Date
            FROM transactions
            WHERE FromId = :accountId OR ToId = :accountId
            ORDER BY Date DESC;
        ";

        var connection = await _connectionProvider.GetConnectionAsync(default);

        var command = new NpgsqlCommand(sql, connection)
            .AddParameter("accountId", accountId)
            .AddParameter("numberTransactions", numberTransactions);

        var transactionHistory = new TransactionHistory();

        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            var transactionId = reader.GetString(0);
            var fromId = reader.IsDBNull(1) ? null : (long?)reader.GetInt64(1);
            var toId = reader.IsDBNull(2) ? null : (long?)reader.GetInt64(2);
            var amount = reader.GetInt64(3);
            var date = reader.GetDateTime(4).ToString("yyyy-MM-dd HH:mm:ss");

            var transaction = new TransactionInfo(transactionId, fromId, toId, amount, date);

            transactionHistory.Transactions.Add(transaction);
        }

        return transactionHistory;
    }

    public async Task RegisterNewTransaction(TransactionInfo transaction)
    {
        const string insertSql = @"
            INSERT INTO transactions (TransactionId, FromId, ToId, Amount, Date)
            VALUES (:transactionId, :fromId, :toId, :amount, :date);
         ";

        var connection = await _connectionProvider.GetConnectionAsync(default);

        var command = new NpgsqlCommand(insertSql, connection)
            .AddParameter("transactionId", transaction.TransactionId)
            .AddParameter("fromId", transaction.FromId ?? (object)DBNull.Value)
            .AddParameter("toId", transaction.ToId ?? (object)DBNull.Value)
            .AddParameter("amount", transaction.Amount)
            .AddParameter("date", DateTime.ParseExact(transaction.Date, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture));

        await command.ExecuteNonQueryAsync();
    }
}