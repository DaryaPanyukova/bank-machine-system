using FluentMigrator;
using Itmo.Dev.Platform.Postgres.Migrations;

namespace BankMachineSystem.BankMachineSystem.Application.DataAccess.Migrations;

[Migration(1, "Initial")]
public class Initial : SqlMigration
{
    protected override string GetUpSql(IServiceProvider serviceProvider) =>
        """
        create table users
        (
            id bigint primary key generated always as identity ,
            name text not null ,
            password text not null ,
            balance bigint not null
            );
        create table admins
        (
            Id bigint primary key generated always as identity ,
            Name text not null ,
            Password text not null
            );
              
        create table transactions
        (
            TransactionId text primary key generated always as identity ,
            FromId bigint,
            ToId bigint,
            Amount bigint not null ,
            Date timestamp not null
            );
        """;

    protected override string GetDownSql(IServiceProvider serviceProvider) =>
        """
        drop table users;
        drop table admins;
        drop table transactions;
        """;
}