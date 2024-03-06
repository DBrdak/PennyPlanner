using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Domain.Users;

namespace Domestica.Budget.Application.Users.RegisterUser
{
    public sealed record RegisterUserCommand(
        string Email,
        string Password,
        string Currency) : ICommand<UserModel>
    {
    }
}
