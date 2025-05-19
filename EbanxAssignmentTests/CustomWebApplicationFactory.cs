using Application;
using Domain;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using WebApi;

namespace EbanxAssignmentTests;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>, IDisposable
{
    public HttpClient CreateClient()
    {
        return CreateDefaultClient();
    }
    
    public void Dispose()
    {
    }
}