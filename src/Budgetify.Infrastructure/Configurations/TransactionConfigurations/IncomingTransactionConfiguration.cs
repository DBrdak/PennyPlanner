using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Budgetify.Domain.Shared.TransactionCategories;
using Budgetify.Domain.Transactions.IncomingTransactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Budgetify.Infrastructure.Configurations.TransactionConfigurations
{
    internal sealed class IncomingTransactionConfiguration : IEntityTypeConfiguration<IncomingTransaction>
    {
        public void Configure(EntityTypeBuilder<IncomingTransaction> builder)
        {
            builder.HasOne(incomingTransaction => incomingTransaction.DestinationAccount)
                .WithMany()
                .HasPrincipalKey(account => account.Id)
                .HasForeignKey(incomingTransaction => incomingTransaction.DestinationAccountId);

            builder.HasOne(incomingTransaction => incomingTransaction.Sender)
                .WithMany()
                .HasPrincipalKey(recipient => recipient.Id)
                .HasForeignKey(incomingTransaction => incomingTransaction.SenderId);

            builder.HasOne(incomingTransaction => incomingTransaction.InternalSourceAccount)
                .WithMany()
                .HasPrincipalKey(account => account.Id)
                .HasForeignKey(incomingTransaction => incomingTransaction.InternalSourceAccountId);

            builder.Property(incomingTransaction => incomingTransaction.Category)
                .HasConversion(category => category.Value, value => IncomingTransactionCategory.FromValue(value));
        }
    }
}
