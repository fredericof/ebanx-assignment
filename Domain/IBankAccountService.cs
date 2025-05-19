namespace Domain;

public interface IBankAccountService
{
    public decimal GetBankAccountBalanceByAccountId(string bankAccountId);
}