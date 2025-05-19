using Application;

namespace Domain;

public interface IEventService
{
    public Task<BankAccount> DepositIntoBankAccountById(string accountId, decimal balance);
    
    public Task<BankAccount> WithdrawFromBankAccountById(string accountId, decimal balance);
    
    public Task<(BankAccount Origin, BankAccount Destination)> TransferFromOriginToDestinationBankAccount(
        string originAccountId,
        string destinationAccountId,
        decimal balance);
}