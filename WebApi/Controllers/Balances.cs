using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("/balance")]
public class Balances : BaseController
{
    [HttpGet]
    public async Task<IResult> GetBalanceByAccountId([FromQuery] string accountId)
    {
        return Results.Ok("Balance");
    }
}