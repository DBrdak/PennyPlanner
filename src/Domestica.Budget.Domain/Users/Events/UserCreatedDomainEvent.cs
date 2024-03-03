using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonAbstractions.DB.Entities;

namespace Domestica.Budget.Domain.Users.Events
{
    public sealed class UserCreatedDomainEvent(UserId UserId) : IDomainEvent
    {
    }
}
