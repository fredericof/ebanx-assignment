namespace Domain;

public interface IBankAccountService
{
    public Task<decimal> GetBankAccountBalanceByAccountIdAsync(string bankAccountId);
}