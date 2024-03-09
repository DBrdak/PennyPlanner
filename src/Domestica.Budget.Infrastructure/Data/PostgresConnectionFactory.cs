﻿using Domestica.Budget.Domain.Users;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Protocols.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Domestica.Budget.Infrastructure.Data
{
    internal sealed class PostgresConnectionFactory : IConnectionFactory
    {
        private readonly string? _formatedConnectionString;
        private const string postgresUrlPattern = @"postgres:\/\/(?<username>[^:]+):(?<password>[^@]+)@(?<host>[^:]+):(?<port>\d+)\/(?<database>[^\/]+)";
        private const string postgresConnectionStringPattern = @"Host=(?<host>[^;]+);Port=(?<port>\d+);Database=(?<database>[^;]+);Username=(?<username>[^;]+);Password=(?<password>[^;]+)";

        internal PostgresConnectionFactory(IConfiguration configuration, IWebHostEnvironment env)
        {
            var connectionString = env.IsDevelopment() ?
                configuration.GetConnectionString("Database") :
                configuration.GetValue<string>("DATABASE_URL") ?? 
                throw new InvalidConfigurationException("Postgres connection string not found");

            if (Regex.IsMatch(connectionString, postgresConnectionStringPattern))
            {
                _formatedConnectionString = connectionString;
            }

            _formatedConnectionString = PostgresConnectionStringFromUrl(connectionString);
        }

        private string PostgresConnectionStringFromUrl(string url)
        {
            if (!Regex.IsMatch(url, postgresUrlPattern))
            {
                ThrowInvalidConnectionStringException();
            }

            var builder = new NpgsqlConnectionStringBuilder(url);

            var host = builder.Host ?? throw ThrowInvalidConnectionStringException();
            var port = builder.Port;
            var database = builder.Database ?? throw ThrowInvalidConnectionStringException();
            var username = builder.Username ?? throw ThrowInvalidConnectionStringException();
            var password = builder.Password ?? throw ThrowInvalidConnectionStringException();

            return GetPostgresConnectionString(host, port, database, username, password);
        }

        private string GetPostgresConnectionString(string host, int port, string database, string username, string password) =>
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