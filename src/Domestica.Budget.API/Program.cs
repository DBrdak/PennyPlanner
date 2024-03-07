using Carter;
using Domestica.Budget.API.Extensions;
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

//app.UseHttpsRedirection();

app.UseRouting();

app.AddHealthChecks();

app.UseAuthentication();

app.UseAuthorization();

app.AddMiddlewares();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapCarter();

await app.RunAsync();
