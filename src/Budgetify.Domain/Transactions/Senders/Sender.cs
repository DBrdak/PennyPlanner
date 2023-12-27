using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Budgetify.Domain.Transactions.Senders;
using CommonAbstractions.DB.Entities;

namespace Budgetify.Domain.Transactions.Senders
{
    public sealed class Sender : Entity
    {
        public SenderName Name { get; private set; }
        public SenderCategory Category { get; private set; }

        public Sender(SenderName name, SenderCategory category) : base()
        {
            Name = name;
            Category = category;
        }
    }
}
