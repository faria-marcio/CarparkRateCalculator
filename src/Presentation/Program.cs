using Application.Extensions;
using Application.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire components.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();
builder.Services.AddApplicationServices();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

app.MapGet("/calculaterate", async (DateTime entry, DateTime exit, IRateCalculator rateCalculator) =>
{
    var rate = rateCalculator.CalculateRate(entry, exit);
    return Results.Ok(rate);
}).WithOpenApi();

app.MapDefaultEndpoints();

app.Run();