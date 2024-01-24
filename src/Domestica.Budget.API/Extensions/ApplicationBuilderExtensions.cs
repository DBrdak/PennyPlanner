using Carter;
using Domestica.Budget.API.Middlewares;
using Domestica.Budget.Application;
using Domestica.Budget.Infrastructure;
using HealthChecks.ApplicationStatus.DependencyInjection;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;

namespace Domestica.Budget.API.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IServiceCollection InjectDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
                .AddApplicationStatus()
                .AddNpgSql(configuration.GetConnectionString("Database") ?? string.Empty);

            services.AddStackExchangeRedisCache(
                options =>
                    options.Configuration = configuration.GetConnectionString("Cache"));

            services.AddInfrastructure(configuration);
            services.AddApplication();
            services.AddCarter();

            services.AddCors(options =>
            {
                options.AddPolicy("DefaultPolicy",
                    builder =>
                    {
                        builder.WithOrigins(
                                "http://localhost:3000")
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials();
                    });
            });

            return services;
        }

        public static async Task<IHost> ApplyMigrations(this IHost app, IHostEnvironment env, int? retry = 0)
        {
            var retryForAvailability = retry.Value;

            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;

            try
            {
                var context = services.GetRequiredService<ApplicationDbContext>();
                await context.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex.Message, "Error occured during migration");

                if (retryForAvailability < 50)
                {
                    retryForAvailability++;
                    Thread.Sleep(2000);
                    await ApplyMigrations(app, env, retryForAvailability);
                }
            }

            return app;
        }

        public static void AddMiddlewares(this IApplicationBuilder app)
        {
            app.UseMiddleware<MonitoringMiddleware>();
            app.UseMiddleware<ExceptionMiddleware>();
        }

        public static void AddHealthChecks(this WebApplication app)
        {
            app.MapHealthChecks(
                "/health",
                new HealthCheckOptions
                {
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
        }
    }
}
