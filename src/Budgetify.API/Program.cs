using Budgetify.API.Extensions;
using Budgetify.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

//builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.ApplyMigrations();
}

app.MapGet("/", () => "Hello World!");

await app.RunAsync();
