using Domestica.Budget.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Domestica.Budget.API.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();

            using var dbContext = scope.ServiceProvider.GetRequiredService<BudgetifyContext>();

            dbContext.Database.Migrate();
        }
    }
}
