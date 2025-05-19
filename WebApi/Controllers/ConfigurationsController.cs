using Application;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("/")]
public class ConfigurationsController : BaseController
{
    private readonly Database _database;
    
    public ConfigurationsController(Database database)
    {
        _database = database;
    }
    
    [HttpPost("reset")]
    public IResult  Reset()
    {
        _database.Reset();
        return Results.Ok();
    }
}