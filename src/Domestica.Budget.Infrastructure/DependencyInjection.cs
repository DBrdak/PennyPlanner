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
using Domestica.Budget.Infrastructure.Email;
using Domestica.Budget.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using AuthenticationOptions = Domestica.Budget.Infrastructure.Authentication.AuthenticationOptions;
using AuthenticationService = Domestica.Budget.Infrastructure.Authentication.AuthenticationService;
using IAuthenticationService = Domestica.Budget.Application.Abstractions.Authentication.IAuthenticationService;

namespace Domestica.Budget.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddPersistence(configuration);

            services.AddAuthentication(configuration);

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

            services.AddScoped<IUserRepository, UserRepository>();

            services.AddTransient<IEmailService, EmailService>();

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());
        }

        private static void AddAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer();

            services.AddAuthorization();

           services.Configure<AuthenticationOptions>(configuration.GetSection("Authentication"));

            services.ConfigureOptions<JwtBearerOptionsSetup>();

            services.Configure<KeycloakOptions>(configuration.GetSection("Keycloak"));

            services.AddTransient<AdminAuthorizationDelegatingHandler>();
            
            services.AddHttpClient<IAuthenticationService, AuthenticationService>((serviceProvider, httpClient) =>
                {
                    var keycloakOptions = serviceProvider.GetRequiredService<IOptions<KeycloakOptions>>().Value;

                    httpClient.BaseAddress = new Uri(keycloakOptions.AdminUrl);
                })
                .AddHttpMessageHandler<AdminAuthorizationDelegatingHandler>();

            services.AddHttpClient<IJwtService, JwtService>((serviceProvider, httpClient) =>
            {
                var keycloakOptions = serviceProvider.GetRequiredService<IOptions<KeycloakOptions>>().Value;

                httpClient.BaseAddress = new Uri(keycloakOptions.TokenUrl);
            });

            services.AddHttpContextAccessor();
            
            services.AddScoped<IUserContext, UserContext>();
        }
    }
}
