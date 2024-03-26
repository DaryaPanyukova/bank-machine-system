using BankMachineSystem.BankMachineSystem.Application.Abstraction;
using BankMachineSystem.BankMachineSystem.Application.DataAccess.Repositories;
using Itmo.Dev.Platform.Postgres.Extensions;
using Itmo.Dev.Platform.Postgres.Models;
using Microsoft.Extensions.DependencyInjection;

namespace BankMachineSystem.BankMachineSystem.Application.DataAccess.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureDataAccess(
        this IServiceCollection collection,
        Action<PostgresConnectionConfiguration> configuration)
    {
        collection.AddPlatformPostgres(builder => builder.Configure(configuration));
        collection.AddPlatformMigrations(typeof(ServiceCollectionExtensions).Assembly);

        collection.AddScoped<IUserRepository, UserRepository>();
        collection.AddScoped<ITransactionRepository, TransactionRepository>();
        collection.AddScoped<IAdminRepository, AdminRepository>();

        return collection;
    }
}