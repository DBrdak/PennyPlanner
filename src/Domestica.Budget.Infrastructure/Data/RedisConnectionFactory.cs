using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Protocols.Configuration;

namespace Domestica.Budget.Infrastructure.Data
{
    internal sealed class RedisConnectionFactory : IConnectionFactory
    {
        private readonly string? _formatedConnectionString;
        private const string redisUrlPattern = @"redis:\/\/\w+:(\w+)@([^:]+):(\d+)";
        private const string redisConnectionStringPattern = @"(?<hostName>[^:]+):(?<portNumber>\d+)(?:,password=(?<password>\w+))?";

        internal RedisConnectionFactory(IConfiguration configuration, IWebHostEnvironment env)
        {
            var connectionString = env.IsDevelopment() ?
                configuration.GetConnectionString("Cache") :
                configuration.GetValue<string>("REDISCLOUD_URL") ??
                throw new InvalidConfigurationException("Redis connection string not found");

            if (Regex.IsMatch(connectionString, redisConnectionStringPattern))
            {
                _formatedConnectionString = connectionString;
            }

            _formatedConnectionString = RedisConnectionStringFromUrl(connectionString);
        }

        private string RedisConnectionStringFromUrl(string url)
        {
            var match = Regex.Match(url, redisUrlPattern);

            if (!match.Success)
            {
                throw new ArgumentException("Provided Redis connectionString is invalid");
            }

            var password = match.Groups[1].Value;
            var hostName = match.Groups[2].Value;
            var portNumber = match.Groups[3].Value;

            return GetRedisConnectionString(hostName, portNumber, password);
        }

        private string GetRedisConnectionString(string hostName, string portNumber, string password) =>
            $"{hostName}:{portNumber},password={password}";

        public void Connect(IServiceCollection services)
        {
            services.AddStackExchangeRedisCache(options => options.Configuration = _formatedConnectionString);
        }
    }
}
