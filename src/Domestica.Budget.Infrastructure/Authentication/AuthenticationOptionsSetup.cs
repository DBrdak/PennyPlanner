using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domestica.Budget.Infrastructure.Authentication
{
    public sealed class AuthenticationOptionsSetup : IConfigureOptions<AuthenticationOptions>
    {
        private const string sectionName = "Authentication";
        private readonly IConfiguration _configuration;

        public AuthenticationOptionsSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(AuthenticationOptions options)
        {
            _configuration.GetSection(sectionName).Bind(options);
        }
    }
}
