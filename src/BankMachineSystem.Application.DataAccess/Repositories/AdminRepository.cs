using BankMachineSystem.BankMachineSystem.Application.Abstraction;
using BankMachineSystem.BankMachineSystem.Application.Models;
using Itmo.Dev.Platform.Postgres.Connection;
using Itmo.Dev.Platform.Postgres.Extensions;
using Npgsql;

namespace BankMachineSystem.BankMachineSystem.Application.DataAccess.Repositories;

public class AdminRepository : IAdminRepository
{
    private readonly IPostgresConnectionProvider _connectionProvider;

    public AdminRepository(IPostgresConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }
    public async Task<AccountInfo?> FindAdmin(long id)
    {
        const string selectSql = @"
            SELECT Id, Name, Password
            FROM admins
            WHERE Id = :id;
        ";

        var connection = await _connectionProvider.GetConnectionAsync(default).ConfigureAwait(false);

        using var command = new NpgsqlCommand(selectSql, connection).AddParameter("id", id);

        using var reader = await command.ExecuteReaderAsync().ConfigureAwait(false);

        if (await reader.ReadAsync().ConfigureAwait(false) is false)
        {
            return null;
        }

        return new AccountInfo(
            Id : reader.GetInt64(0),
            Name: reader.GetString(1),
            Password: reader.GetString(2)
        );
    }
}