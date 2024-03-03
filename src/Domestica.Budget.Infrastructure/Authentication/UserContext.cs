using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domestica.Budget.Application.Abstractions.Authentication;

namespace Domestica.Budget.Infrastructure.Authentication
{
    public sealed class UserContext : IUserContext
    {
        public string IdentityId { get; }
    }
}
