using Application;
using Domain;

namespace WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container
        builder.Services.AddControllers();
        builder.Services.AddOpenApi();
        builder.Services.AddSwaggerGen();

        // Dependency Injection
        builder.Services.AddSingleton<BankAccountRepository>();
        builder.Services.AddScoped<IBankAccountService, BankAccountService>();
        builder.Services.AddScoped<IEventService, EventService>();

        var app = builder.Build();

        // Configure the HTTP request pipeline
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        else
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.MapControllers();

        app.Run();
    }
}