using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Budgetify.Domain.Transactions.IncomingTransactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Budgetify.Infrastructure.Configurations.TransactionConfigurations
{
    internal sealed class IncomingTransactionConfiguration : IEntityTypeConfiguration<IncomingTransaction>
    {
        public void Configure(EntityTypeBuilder<IncomingTransaction> builder)
        {
            builder.HasOne(incomingTransaction => incomingTransaction.Sender)
                .WithMany()
                .HasForeignKey(incomingTransaction => incomingTransaction.SenderId);

            builder.HasOne(incomingTransaction => incomingTransaction.InternalSourceAccount)
                .WithMany()
                .HasForeignKey(incomingTransaction => incomingTransaction.InternalSourceAccountId);

            builder.Property(incomingTransaction => incomingTransaction.Category)
                .HasConversion(category => category.Value, value => IncomingTransactionCategory.FromValue(value));
        }
    }
}
