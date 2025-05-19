using System.Collections.Concurrent;
using Domain;

namespace Application;

public class Database
{
    private ConcurrentDictionary<String, BankAccount> _bankAccountsDB;

    public Database()
    {
        _bankAccountsDB = new();
    }

    public BankAccount GetBankAccountById(string accountId)
    {
        _bankAccountsDB.TryGetValue(accountId, out BankAccount? bankAccount);

        if (bankAccount == null)
            throw new BankAccountNotFoundException();

        return bankAccount;
    }

    public void Reset()
    {
        _bankAccountsDB.Clear();
    }
}