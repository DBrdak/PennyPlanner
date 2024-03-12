using CommonAbstractions.DB;
using Domestica.Budget.Application.Abstractions.Authentication;
using Domestica.Budget.Application.Abstractions.Email;
using Domestica.Budget.Application.Abstractions.Messaging.Caching;
using Domestica.Budget.Domain.Accounts;
using Domestica.Budget.Domain.BudgetPlans;
using Domestica.Budget.Domain.TransactionCategories;
using Domestica.Budget.Domain.TransactionEntities;
using Domestica.Budget.Domain.Transactions;
using Domestica.Budget.Domain.TransactionSubcategories;
using Domestica.Budget.Domain.Users;
using Domestica.Budget.Infrastructure.Authentication;
using Domestica.Budget.Infrastructure.Data;
using Domestica.Budget.Infrastructure.Email;
using Domestica.Budget.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols.Configuration;
using Npgsql;
using StackExchange.Redis;
using System.Text.RegularExpressions;
using AuthenticationOptions = Domestica.Budget.Infrastructure.Authentication.AuthenticationOptions;
using IPasswordService = Domestica.Budget.Application.Abstractions.Authentication.IPasswordService;

namespace Domestica.Budget.Infrastructure
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
