using System.Collections.Concurrent;
using Domain;

namespace Application;

public class BankAccountRepository
{
    private ConcurrentDictionary<String, BankAccount> _bankAccountsDB;

    public BankAccountRepository()
    {
        _bankAccountsDB = new();
    }

    public BankAccount? GetBankAccountById(string accountId)
    {
        _bankAccountsDB.TryGetValue(accountId, out BankAccount? bankAccount);
        if (bankAccount == null)
            return null;
        return bankAccount;
    }
    
    public BankAccount? AddBankAccount(string accountId)
    {
        _bankAccountsDB.TryAdd(accountId, new BankAccount());
        return GetBankAccountById(accountId);
    }
    
    public BankAccount UpdateBankAccountBalance(string accountId, decimal amount)
    {
        return _bankAccountsDB.AddOrUpdate(
            accountId,
            new BankAccount { Id = accountId, Balance = amount },
            (key, existingAccount) =>
            {
                existingAccount.Id = accountId;
                existingAccount.Balance += amount;

                if (existingAccount.Balance < 0)
                    existingAccount.Balance = 0;
                
                return existingAccount;
            });
    }
    
    public (BankAccount Origin, BankAccount Destination) TransferBetweenAccounts(
        string originAccountId, 
        string destinationAccountId, 
        decimal amount)
    {
        if (amount < 0)
            throw new ArgumentException("Amount cannot be negative.");
        
        BankAccount originAccount = _bankAccountsDB.AddOrUpdate(
            originAccountId,
            new BankAccount { Id = originAccountId, Balance = 0 },
            (key, existingAccount) =>
            {
                existingAccount.Id = originAccountId;
                existingAccount.Balance -= amount;
                if (existingAccount.Balance < 0)
                    throw new InvalidOperationException("Insufficient balance in origin account.");
                return existingAccount;
            });
        
        BankAccount destinationAccount = _bankAccountsDB.AddOrUpdate(
            destinationAccountId,
            new BankAccount { Id = destinationAccountId, Balance = amount },
            (key, existingAccount) =>
            {
                existingAccount.Id = destinationAccountId;
                existingAccount.Balance += amount;
                return existingAccount;
            });

        return (originAccount, destinationAccount);
    }

    public void Reset()
    {
        _bankAccountsDB.Clear();
    }
}