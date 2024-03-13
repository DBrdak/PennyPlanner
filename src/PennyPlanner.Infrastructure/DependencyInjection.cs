using CommonAbstractions.DB;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PennyPlanner.Application.Abstractions.Authentication;
using PennyPlanner.Application.Abstractions.Email;
using PennyPlanner.Application.Abstractions.Messaging.Caching;
using PennyPlanner.Domain.Accounts;
using PennyPlanner.Domain.BudgetPlans;
using PennyPlanner.Domain.TransactionCategories;
using PennyPlanner.Domain.TransactionEntities;
using PennyPlanner.Domain.Transactions;
using PennyPlanner.Domain.TransactionSubcategories;
using PennyPlanner.Domain.Users;
using PennyPlanner.Infrastructure.Authentication;
using PennyPlanner.Infrastructure.Data;
using PennyPlanner.Infrastructure.Email;
using PennyPlanner.Infrastructure.Repositories;
using IPasswordService = PennyPlanner.Application.Abstractions.Authentication.IPasswordService;

namespace PennyPlanner.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration,
            IWebHostEnvironment env)
        {
            services.AddPersistence(configuration, env);

            services.AddAuthentication(configuration);

            services.Configure<EmailProviderOptions>(configuration.GetSection("SendGrid"));

            return services;
        }

        private static void AddPersistence(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
        {
            new PostgresConnectionFactory(configuration, env).Connect(services);
            new RedisConnectionFactory(configuration, env).Connect(services);

            services.AddDataProtection();

            services.AddScoped<IAccountRepository, AccountRepository>();

            services.AddScoped<ITransactionEntityRepository, TransactionEntityRepository>();

            services.AddScoped<ITransactionRepository, TransactionRepository>();

            services.AddScoped<IBudgetPlanRepository, BudgetPlanRepository>();

            services.AddScoped<ITransactionCategoryRepository, TransactionCategoryRepository>();

            services.AddScoped<ITransactionSubcategoryRepository, TransactionSubcategoryRepository>();

            services.AddScoped<ICacheRepository, CacheRepository>();

            services.AddScoped<IUserRepository, UserRepository>();

            services.AddTransient<IEmailService, EmailService>();

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());
        }

        private static void AddAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureOptions<AuthenticationOptionsSetup>();
            services.ConfigureOptions<JwtBearerOptionsSetup>();

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer();

            services.AddAuthorization();

            services.AddHttpContextAccessor();

            services.AddScoped<IUserContext, UserContext>();

            services.AddScoped<IPasswordService, PasswordService>();

            services.AddScoped<IJwtService, JwtService>();
        }

    }
}
