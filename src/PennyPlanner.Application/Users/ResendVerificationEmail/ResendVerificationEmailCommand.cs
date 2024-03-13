using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonAbstractions.DB.Messaging;
using PennyPlanner.Domain.Users;

namespace PennyPlanner.Application.Users.ResendVerificationEmail
{
    public sealed record ResendVerificationEmailCommand(string Email) : ICommand<User>
    {

    }
}
