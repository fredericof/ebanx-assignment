using System.Collections.Concurrent;
using Domain;

namespace Application;

public class Database
{
    public ConcurrentDictionary<String, BankAccount> BankAccountsDB { get; set; } = new();
}