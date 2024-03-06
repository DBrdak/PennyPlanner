using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Domain.Users;

namespace Domestica.Budget.Application.Users.LogInUser
{
    public sealed record LogInUserCommand(string Email,string Password) : ICommand<AccessToken>
    {
    }
}
