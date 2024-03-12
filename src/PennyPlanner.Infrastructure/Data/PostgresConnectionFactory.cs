using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Protocols.Configuration;

namespace PennyPlanner.Infrastructure.Data
{
    internal sealed class PostgresConnectionFactory : IConnectionFactory
    {
        private readonly string? _formatedConnectionString;
        private const string postgresUrlPattern = @"postgres:\/\/(?<username>[^:]+):(?<password>[^@]+)@(?<host>[^:]+):(?<port>\d+)\/(?<database>[^\/]+)";
        private const string postgresConnectionStringPattern = @"Host=(?<host>[^;]+);Port=(?<port>\d+);Database=(?<database>[^;]+);Username=(?<username>[^;]+);Password=(?<password>[^;]+)";

        internal PostgresConnectionFactory(IConfiguration configuration, IWebHostEnvironment env)
        {
            var connectionString = env.IsDevelopment() ?
                configuration.GetConnectionString("Database") ??
                throw new InvalidConfigurationException("Postgres connection string not found") :
                configuration.GetValue<string>("DATABASE_URL") ?? 
                throw new InvalidConfigurationException("Postgres connection string not found");

            if (Regex.IsMatch(connectionString, postgresConnectionStringPattern))
            {
                _formatedConnectionString = connectionString;
                return;
            }

            _formatedConnectionString = PostgresConnectionStringFromUrl(connectionString);
        }

        private string PostgresConnectionStringFromUrl(string url)
        {
            var match = Regex.Match(url, postgresUrlPattern);

            if (!match.Success)
            {
                ThrowInvalidConnectionStringException();
            }

            var host = match.Groups["host"].Value ?? throw ThrowInvalidConnectionStringException();
            var port = match.Groups["port"].Value;
            var database = match.Groups["database"].Value ?? throw ThrowInvalidConnectionStringException();
            var username = match.Groups["username"].Value ?? throw ThrowInvalidConnectionStringException();
            var password = match.Groups["password"].Value ?? throw ThrowInvalidConnectionStringException();

            return GetPostgresConnectionString(host, port, database, username, password);
        }

        private string GetPostgresConnectionString(string host, string port, string database, string username, string password) =>
            $"Host={host};Port={port};Database={database};Username={username};Password={password}";

        private Exception ThrowInvalidConnectionStringException() =>
            throw new ArgumentException("Provided Postgres connectionString is invalid");

        public void Connect(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(_formatedConnectionString, o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery))
                    .UseSnakeCaseNamingConvention();
            });
        }
    }
}
