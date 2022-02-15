using Api;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<LeakyBucket>(new LeakyBucket(10, 1));

var app = builder.Build();

app.UseMiddleware<LeakyBucketRateLimiterMiddleware>();

app.MapPost("/return-my-number", ([FromBody] int number) =>
{
    app.Logger.LogInformation($"Dispatched request at {TimeOnly.FromDateTime(DateTime.UtcNow)}");

    return number;
});

app.Run();

public partial class Program { }
