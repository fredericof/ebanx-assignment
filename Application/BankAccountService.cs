using Domain;

namespace Application;

public class BankAccountService : IBankAccountService
{
    private readonly BankAccountRepository _bankAccountRepository;

    public BankAccountService(BankAccountRepository bankAccountRepository)
    {
        _bankAccountRepository = bankAccountRepository;
    }
    
    public async Task<decimal> GetBankAccountBalanceByAccountIdAsync(string bankAccountId)
    {
        BankAccount? bankAccount = _bankAccountRepository.GetBankAccountById(bankAccountId);

        if (bankAccount == null)
            throw new BankAccountNotFoundException();
        
        return bankAccount.Balance;
    }
}