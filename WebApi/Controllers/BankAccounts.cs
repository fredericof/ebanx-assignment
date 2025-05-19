using Domain;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("/balance")]
public class BankAccounts : BaseController
{
    private readonly IBankAccountService _bankAccountService;
    
    public BankAccounts(IBankAccountService bankAccountService)
    {
        _bankAccountService = bankAccountService;
    }
    
    [HttpGet]
    public async Task<IResult> GetBalanceByAccountId([FromQuery] string account_id)
    {
        decimal balance = 0;
        
        try
        {
            balance = await _bankAccountService.GetBankAccountBalanceByAccountIdAsync(account_id);
        }
        catch (BankAccountNotFoundException e)
        {
            return Results.NotFound(balance);
        }
        
        return Results.Ok(balance);
    }
}