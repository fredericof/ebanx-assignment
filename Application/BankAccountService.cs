using Domain;

namespace Application;

public class BankAccountService : IBankAccountService
{
    private readonly Database _database;

    public BankAccountService(Database database)
    {
        _database = database;
    }
    
    public async Task<decimal> GetBankAccountBalanceByAccountIdAsync(string bankAccountId)
    {
        return _database.GetBankAccountById(bankAccountId).Balance;
    }
}