using System.Text.Json.Serialization;
using Budgetify.Infrastructure;
using Carter;
using Domestica.Budget.API.Extensions;
using Domestica.Budget.Application;
using Domestica.Budget.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddCarter();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();
}

app.MapCarter();

await app.RunAsync();
