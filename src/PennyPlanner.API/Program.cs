using Carter;
using PennyPlanner.API.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.UseSerilog((context, loggerConfig) =>
    loggerConfig.ReadFrom.Configuration(context.Configuration));

builder.Services.InjectDependencies(builder.Configuration, builder.Environment);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

await app.ApplyMigrations();

app.UseCors("DefaultPolicy");

//app.UseHttpsRedirection();

app.Run(async (context) =>
{
    context.Response.ContentType = "text/html";
    await context.Response.SendFileAsync(Path.Combine(app.Environment.WebRootPath, "index.html"));
});

app.UseRouting();

app.AddHealthChecks();

app.UseAuthentication();

app.UseAuthorization();

app.AddMiddlewares();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapCarter();

await app.RunAsync();
