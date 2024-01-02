using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Budgetify.Domain.TransactionEntities;
using Budgetify.Domain.TransactionEntities.TransactionRecipients;
using Budgetify.Domain.TransactionEntities.TransactionSenders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Budgetify.Infrastructure.Configurations.TransactionEntitiesConfiguration
{
    internal sealed class TransactionEntitiesConfiguration : IEntityTypeConfiguration<TransactionEntity>
    {
        public void Configure(EntityTypeBuilder<TransactionEntity> builder)
        {
            builder.ToTable("transaction_entities");

            builder.HasKey(t => t.Id);

            builder.Property(transactionEntity => transactionEntity.Name)
                .HasConversion(name => name.Value, value => new TransactionEntityName(value));

            builder.HasMany(transactionEntity => transactionEntity.Transactions)
                .WithOne()
                .HasPrincipalKey(transactionEntity => transactionEntity.Id)
                .HasForeignKey(transaction => transaction.Id);

            builder.HasDiscriminator<string>("entity_type")
                .HasValue<TransactionSender>(nameof(TransactionSender))
                .HasValue<TransactionRecipient>(nameof(TransactionRecipient));
        }
    }
}
