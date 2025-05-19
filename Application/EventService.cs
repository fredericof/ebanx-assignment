using Domain;

namespace Application;

public class EventService : IEventService
{
    private readonly BankAccountRepository _bankAccountRepository;

    public EventService(BankAccountRepository bankAccountRepository)
    {
        _bankAccountRepository = bankAccountRepository;
    }

    public async Task<BankAccount> DepositIntoBankAccountById(string accountId, decimal amount)
    {
        BankAccount? bankAccount = _bankAccountRepository.GetBankAccountById(accountId);

        if (bankAccount == null)
            _bankAccountRepository.AddBankAccount(accountId);
        
        return _bankAccountRepository.UpdateBankAccountBalance(accountId, amount);
    }

    public async Task<BankAccount> WithdrawFromBankAccountById(string accountId, decimal amount)
    {
        BankAccount? bankAccount = _bankAccountRepository.GetBankAccountById(accountId);

        if (bankAccount == null)
            throw new BankAccountNotFoundException();
        
        return _bankAccountRepository.UpdateBankAccountBalance(accountId, amount * -1);
    }

    public async Task<(BankAccount Origin, BankAccount Destination)> TransferFromOriginToDestinationBankAccount(
        string originAccountId, 
        string destinationAccountId, 
        decimal amount)
    {
        BankAccount? bankAccount = _bankAccountRepository.GetBankAccountById(originAccountId);
        
        if (bankAccount == null)
            throw new BankAccountNotFoundException();

        (BankAccount origin, BankAccount destination) =
            _bankAccountRepository.TransferBetweenAccounts(originAccountId, destinationAccountId, amount);

        return (Origin: origin, Destination: destination);
    }
}