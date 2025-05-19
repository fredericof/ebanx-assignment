using Application;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("/")]
public class ConfigurationsController : BaseController
{
    private readonly BankAccountRepository _bankAccountRepository;
    
    public ConfigurationsController(BankAccountRepository bankAccountRepository)
    {
        _bankAccountRepository = bankAccountRepository;
    }
    
    [HttpPost("reset")]
    public IResult  Reset()
    {
        _bankAccountRepository.Reset();
        return Results.Ok();
    }
}