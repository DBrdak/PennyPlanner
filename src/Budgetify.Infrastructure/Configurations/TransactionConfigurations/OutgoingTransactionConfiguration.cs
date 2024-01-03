using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Budgetify.Domain.Transactions;
using Budgetify.Domain.Transactions.OugoingTransactions;
using CommonAbstractions.DB.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Budgetify.Infrastructure.Configurations.TransactionConfigurations
{
    internal sealed class OutgoingTransactionConfiguration : IEntityTypeConfiguration<OutgoingTransaction>
    {
        public void Configure(EntityTypeBuilder<OutgoingTransaction> builder)
        {
            builder.HasOne(outgoingTransaction => outgoingTransaction.SourceAccount)
                .WithMany()
                .HasPrincipalKey(account => account.Id)
                .HasForeignKey(outgoingTransaction => outgoingTransaction.SourceAccountId);

            builder.HasOne(outgoingTransaction => outgoingTransaction.Recipient)
                .WithMany()
                .HasPrincipalKey(recipient => recipient.Id)
                .HasForeignKey(outgoingTransaction => outgoingTransaction.RecipientId);

            builder.HasOne(outgoingTransaction => outgoingTransaction.InternalDestinationAccount)
                .WithMany()
                .HasPrincipalKey(account => account.Id)
                .HasForeignKey(outgoingTransaction => outgoingTransaction.InternalDestinationAccountId);


            builder.Property(outgoingTransaction => outgoingTransaction.Category)
                .HasConversion(category => category.Value, value => OutgoingTransactionCategory.FromValue(value));
        }
    }
}
