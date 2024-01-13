using System.Text.Json.Serialization;
using Carter;
using Domestica.Budget.API.Extensions;
using Domestica.Budget.Application;
using Domestica.Budget.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.UseSerilog((context, loggerConfig) =>
    loggerConfig.ReadFrom.Configuration(context.Configuration));

builder.Services.InjectDependencies(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    await app.ApplyMigrations(app.Environment);
}

app.UseCors("DefaultPolicy");
app.UseRouting();
app.AddHealthChecks();
app.MapCarter();
app.AddMiddlewares();

await app.RunAsync();
