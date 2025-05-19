using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("/")]
public class ConfigurationsController : BaseController
{
    public ConfigurationsController()
    {
        
    }
    
    [HttpPost("reset")]
    public IResult  Reset()
    {
        return Results.Ok();
    }
}