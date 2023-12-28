using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Budgetify.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddPersistence(configuration);

            return services;
        }

        private static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString =
                configuration.GetConnectionString("Database") ??
                throw new ArgumentNullException(nameof(configuration));

            services.AddDbContext<BudgetifyContext>(options =>
            {
                options.UseNpgsql(connectionString).UseSnakeCaseNamingConvention();
            });
        }
    }
}
