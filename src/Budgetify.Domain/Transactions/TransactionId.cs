using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonAbstractions.DB.Entities;

namespace Budgetify.Domain.Transactions
{
    public sealed record TransactionId : EntityId
    {
        public TransactionId(Guid value) : base(value)
        {
            
        }

        public TransactionId() : base(Guid.NewGuid())
        { }
    }
}
