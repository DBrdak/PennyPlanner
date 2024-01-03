using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonAbstractions.DB.Entities;

namespace Budgetify.Domain.Accounts
{
    public sealed record AccountId : EntityId
    {
        public AccountId(Guid value) : base(value)
        {

        }

        public AccountId() : base(Guid.NewGuid())
        {
            
        }
    }
}
