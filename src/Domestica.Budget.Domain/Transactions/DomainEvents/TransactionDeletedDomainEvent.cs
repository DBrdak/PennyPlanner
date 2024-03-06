using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonAbstractions.DB.Entities;

namespace Domestica.Budget.Domain.Transactions.DomainEvents
{
    public sealed record TransactionDeletedDomainEvent(Transaction DeletedTransaction) : IDomainEvent
    {
    }
}
