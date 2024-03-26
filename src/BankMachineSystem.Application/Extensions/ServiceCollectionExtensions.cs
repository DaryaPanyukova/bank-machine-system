using BankMachineSystem.BankMachineSystem.Application.Admins;
using BankMachineSystem.BankMachineSystem.Application.Contracts.Admins;
using BankMachineSystem.BankMachineSystem.Application.Contracts.Users;
using BankMachineSystem.BankMachineSystem.Application.Users;
using BankMachineSystem.BankMachineSystem.Application.Contracts;
using Microsoft.Extensions.DependencyInjection;


namespace BankMachineSystem.BankMachineSystem.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection collection)
    {
        collection.AddScoped<IUserService, UserService>();
        collection.AddScoped<IAdminService, AdminService>();

        collection.AddSingleton<CurrentAccountManager>();
        collection.AddSingleton<ICurrentAccountService>(
            p => p.GetRequiredService<CurrentAccountManager>());
    //    collection.AddScoped<CurrentAccountManager>();
   //     collection.AddScoped<ICurrentAccountService>(
    //        p => p.GetRequiredService<CurrentAccountManager>());
        return collection;
    }
}