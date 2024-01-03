using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonAbstractions.DB.Entities;

namespace Budgetify.Domain.TransactionEntities
{
    public sealed record TransactionEntityId : EntityId
    {
        public TransactionEntityId(Guid value) : base(value)
        {
            
        }

        public TransactionEntityId() : base(Guid.NewGuid())
        { }
    }
}
