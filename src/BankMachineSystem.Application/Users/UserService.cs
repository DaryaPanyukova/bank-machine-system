using BankMachineSystem.BankMachineSystem.Application.Abstraction;
using BankMachineSystem.BankMachineSystem.Application.Contracts.Users;
using BankMachineSystem.BankMachineSystem.Application.Contracts;
using BankMachineSystem.BankMachineSystem.Application.Models;
using BankMachineSystem.BankMachineSystem.Application.Models.Transactions;
using BankMachineSystem.BankMachineSystem.Application.Models.TransactionsWithNames;

namespace BankMachineSystem.BankMachineSystem.Application.Users;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly ITransactionRepository _transactionRepository;
    private readonly CurrentAccountManager _currentAccountInfo;

    public UserService(IUserRepository userRepository, ITransactionRepository transactionRepository,
        CurrentAccountManager currentAccountInfo)
    {
        _userRepository = userRepository;
        _transactionRepository = transactionRepository;
        _currentAccountInfo = currentAccountInfo;
    }

    public async Task<LoginResult> Login(long id, string password)
    {
        var user = await _userRepository.FindUser(id);

        if (user is null)
        {
            return new LoginResult.Failed.AccountNotFound();
        }

        if (user.Account.Password != password)
        {
            return new LoginResult.Failed.WrongPassword();
        }

        _currentAccountInfo.AccountInfo = user.Account;
        _currentAccountInfo.Role = Role.User;
        return new LoginResult.Success();
    }

    public async Task<UserInfo> GetAccountInfo()
    {
        return await _userRepository.FindUser(_currentAccountInfo.AccountInfo.Id);
    }

    public async Task<TransactionsWithNamesHistory> GetTransactionHistory(int numberTransactions)
    {
        var history =
            await _transactionRepository.GetLastTransactions(_currentAccountInfo.AccountInfo.Id, numberTransactions);
        var historyWithNames = new TransactionsWithNamesHistory();
        foreach (TransactionInfo transaction in history.Transactions)
        {
            var userFrom = transaction.FromId == null ? null : await _userRepository.FindUser(transaction.FromId.Value);
            var userTo = transaction.FromId == null ? null : await _userRepository.FindUser(transaction.ToId.Value);

            var transactionWithNames =
                new TransactionWithNames(transaction, userFrom?.Account.Name, userTo?.Account.Name);
            historyWithNames.Transactions.Add(transactionWithNames);
        }

        return historyWithNames;
    }

    public async Task<WithdrawMoneyResult> WithdrawMoney(int amount)
    {
        var withdrawResult = await _userRepository.WithdrawMoney(_currentAccountInfo.AccountInfo.Id, amount);
        await _transactionRepository.RegisterNewTransaction(new TransactionInfo(
            FromId: _currentAccountInfo.AccountInfo.Id,
            ToId: null,
            Amount: amount,
            Date: DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            TransactionId: Guid.NewGuid().ToString()
        ));
        return withdrawResult ? new WithdrawMoneyResult.Success() : new WithdrawMoneyResult.Failed.InsufficientFunds();
    }

    public async Task<TopUpResult> TopUpBalance(long id, int amount)
    {
        var user = await _userRepository.FindUser(id);

        if (user is null)
        {
            return new TopUpResult.Failed.AccountNotFound();
        }

        await _userRepository.TopUpBalance(user.Account.Id, amount);
        await _transactionRepository.RegisterNewTransaction(new TransactionInfo(
            FromId: null,
            ToId: id,
            Amount: amount,
            Date: DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            TransactionId: Guid.NewGuid().ToString()
        ));
        return new TopUpResult.Success();
    }
}