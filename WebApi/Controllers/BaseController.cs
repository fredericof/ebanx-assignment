using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Produces("application/json")]
[Consumes("application/json")]
public abstract class BaseController
{
    public BaseController()
    {
        
    }
}