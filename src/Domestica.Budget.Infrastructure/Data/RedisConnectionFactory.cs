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
using StackExchange.Redis;

namespace Domestica.Budget.Infrastructure.Data
{
    internal sealed class RedisConnectionFactory : IConnectionFactory
    {
        private readonly string _connectionString;
        private const string redisUrlPattern = @"redis:\/\/\w+:(\w+)@([^:]+):(\d+)";
        private const string redisConnectionStringPattern = @"(?<hostName>[^:]+):(?<portNumber>\d+)(?:,password=(?<password>\w+))?";

        internal RedisConnectionFactory(IConfiguration configuration, IWebHostEnvironment env)
        {
            var connectionString = env.IsDevelopment() ?
                configuration.GetConnectionString("Cache") ??
                throw new InvalidConfigurationException("Redis connection string not found") :
                configuration.GetValue<string>("REDISCLOUD_URL") ??
                throw new InvalidConfigurationException("Redis connection string not found");

            _connectionString = connectionString;
        }

        public void Connect(IServiceCollection services)
        {
            services.AddStackExchangeRedisCache(options => 
                options.ConnectionMultiplexerFactory = async () => 
                    await ConnectionMultiplexer.ConnectAsync(_connectionString));
        }
    }
}
