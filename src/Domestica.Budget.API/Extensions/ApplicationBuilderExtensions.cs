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
        public static IServiceCollection InjectDependencies(
            this IServiceCollection services,
            IConfiguration configuration,
            IWebHostEnvironment env)
        {
            services.AddHealthChecks()
                .AddApplicationStatus()
                .AddNpgSql(configuration.GetConnectionString("Database") ?? string.Empty)
                .AddRedis(configuration.GetConnectionString("Cache") ?? string.Empty);

            services.AddInfrastructure(configuration, env);
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

        public static async Task<IHost> ApplyMigrations(this IHost app, int? retry = 0)
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
                    await ApplyMigrations(app, retryForAvailability);
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

        public static void SecureApp(this WebApplication app)
        {
            app.UseXContentTypeOptions();
            app.UseReferrerPolicy(opt => opt.NoReferrer());
            app.UseXXssProtection(opt => opt.EnabledWithBlockMode());
            app.UseXfo(opt => opt.Deny());
            app.UseCspReportOnly(opt => opt
                .BlockAllMixedContent()
                .StyleSources(s => s.Self().CustomSources("https://fonts.googleapis.com"))
                .FontSources(s => s.Self().CustomSources("https://fonts.gstatic.com", "data:"))
                .FormActions(s => s.Self())
                .FrameAncestors(s => s.Self())
                .ScriptSources(s => s.Self()));

            if (!app.Environment.IsDevelopment())
            {
                app.Use(
                    async (context, next) =>
                    {
                        context.Response.Headers.Add("Strict-Transport-Security", "max-age=31536000");
                        await next.Invoke();
                    });
            }
        }
    }
}
