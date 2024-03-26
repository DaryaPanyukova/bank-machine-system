using BankMachineSystem.BankMachineSystem.Application.Abstraction;
using BankMachineSystem.BankMachineSystem.Application.Contracts.Users;
using BankMachineSystem.BankMachineSystem.Application.Models;
using Itmo.Dev.Platform.Postgres.Connection;
using Itmo.Dev.Platform.Postgres.Extensions;
using Npgsql;

namespace BankMachineSystem.BankMachineSystem.Application.DataAccess.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IPostgresConnectionProvider _connectionProvider;

    public UserRepository(IPostgresConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }

    public async Task<UserInfo?> FindUser(long userId)
    {
        const string sql = """
            select id, name, password, balance
            from users
            where id = :userId;
        """;

        var connection = await _connectionProvider.GetConnectionAsync(default);
        var command = new NpgsqlCommand(sql, connection).AddParameter("userId", userId);

        await using var reader = await command.ExecuteReaderAsync();

        if (await reader.ReadAsync())
        {
            return new UserInfo(
                Account: new AccountInfo(
                    Id: reader.GetInt64(0),
                    Name: reader.GetString(1),
                    Password: reader.GetString(2)
                ),
                Balance: reader.GetInt32(3)
            );
        }

        return null;
    }

    public async Task<bool> WithdrawMoney(long id, int amount)
    {
        const string selectSql = @"
        SELECT bankAccountNumber, Balance
        FROM users
        WHERE BankAccountNumber = :bankAccountNumber;
    ";

        const string updateSql = @"
        UPDATE users
        SET Balance = Balance - :amount
        WHERE BankAccountNumber = :bankAccountNumber;
    ";

        var connection = await _connectionProvider.GetConnectionAsync(default);

        var selectCommand =
            new NpgsqlCommand(selectSql, connection).AddParameter("id", id);

        await using var selectReader = await selectCommand.ExecuteReaderAsync();

        if (await selectReader.ReadAsync())
        {
            var currentBalance = selectReader.GetInt32(1);

            if (currentBalance >= amount)
            {
                var updateCommand = new NpgsqlCommand(updateSql, connection)
                    .AddParameter("id", id)
                    .AddParameter("amount", amount);

                await updateCommand.ExecuteNonQueryAsync();
                return true;
            }
        }

        return false;
    }

    public async Task TopUpBalance(long id, int amount)
    {
        const string updateSql = @"
        UPDATE users
        SET Balance = Balance + :amount
        WHERE Id = :id;
    ";

        var connection = await _connectionProvider.GetConnectionAsync(default);

        var updateCommand = new NpgsqlCommand(updateSql, connection)
            .AddParameter("bankAccountNumber", id)
            .AddParameter("amount", amount);

        await updateCommand.ExecuteNonQueryAsync();
    }

    public async Task<CreateAccountResult> CreateNewAccount(AccountInfo accountInfo)
    {
        const string selectSql = @"
        SELECT COUNT(*)
        FROM users
        WHERE Id = :AccountId;
    ";

        const string insertSql = @"
        INSERT INTO users (Id, Name, Password, Balance)
        VALUES (:AccountId, :Name, :Password, :0);
    ";

        var connection = await _connectionProvider.GetConnectionAsync(default);

        var selectCommand = new NpgsqlCommand(selectSql, connection)
            .AddParameter("AccountId", accountInfo.Id);

        var userCount = (long)await selectCommand.ExecuteScalarAsync();

        if (userCount > 0)
        {
            return new CreateAccountResult.Failed.IdExists();
        }

        var insertCommand = new NpgsqlCommand(insertSql, connection)
            .AddParameter("AccountId", accountInfo.Id)
            .AddParameter("Name", accountInfo.Name)
            .AddParameter("Password", accountInfo.Password);

        await insertCommand.ExecuteNonQueryAsync();

        return new CreateAccountResult.Success();
    }
}