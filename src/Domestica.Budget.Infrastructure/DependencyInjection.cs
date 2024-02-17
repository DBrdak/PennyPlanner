using CommonAbstractions.DB;
using Domestica.Budget.Application.Caching;
using Domestica.Budget.Domain.Accounts;
using Domestica.Budget.Domain.BudgetPlans;
using Domestica.Budget.Domain.TransactionCategories;
using Domestica.Budget.Domain.TransactionEntities;
using Domestica.Budget.Domain.Transactions;
using Domestica.Budget.Domain.TransactionSubcategories;
using Domestica.Budget.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Domestica.Budget.Infrastructure
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

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(connectionString, o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery))
                    .UseSnakeCaseNamingConvention();
            });

            services.AddStackExchangeRedisCache(options =>
                options.Configuration = configuration.GetConnectionString("Cache"));

            services.AddScoped<IAccountRepository, AccountRepository>();

            services.AddScoped<ITransactionEntityRepository, TransactionEntityRepository>();

            services.AddScoped<ITransactionRepository, TransactionRepository>();

            services.AddScoped<IBudgetPlanRepository, BudgetPlanRepository>();

            services.AddScoped<ITransactionCategoryRepository, TransactionCategoryRepository>();

            services.AddScoped<ITransactionSubcategoryRepository, TransactionSubcategoryRepository>();

            services.AddScoped<ICacheRepository, CacheRepository>();

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());
        }
    }
}
