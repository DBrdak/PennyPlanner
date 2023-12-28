using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Budgetify.Domain.Transactions.Senders;
using CommonAbstractions.DB.Entities;

namespace Budgetify.Domain.Transactions.Senders
{
    public sealed class Sender(SenderName name) : Entity;
}
