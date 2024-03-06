﻿using Domestica.Budget.Domain.TransactionEntities;
using Domestica.Budget.Domain.TransactionEntities.TransactionRecipients;
using Domestica.Budget.Domain.TransactionEntities.TransactionSenders;
using Domestica.Budget.Domain.Transactions;
using Domestica.Budget.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domestica.Budget.Infrastructure.Configurations
{
    internal sealed class TransactionEntitiesConfiguration : IEntityTypeConfiguration<TransactionEntity>
    {
        public void Configure(EntityTypeBuilder<TransactionEntity> builder)
        {
            builder.ToTable("transaction_entities");

            builder.HasKey(t => t.Id);

            builder.Property(transactionEntity => transactionEntity.Id)
                .HasConversion(id => id.Value, value => new TransactionEntityId(value));

            builder.Property(transactionEntity => transactionEntity.Name)
                .HasConversion(name => name.Value, value => new TransactionEntityName(value));

            builder.HasMany(transactionEntity => transactionEntity.Transactions)
                .WithOne()
                .HasPrincipalKey(transactionEntity => transactionEntity.Id)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany<Transaction>()
                .WithOne()
                .HasPrincipalKey(transactionEntity => transactionEntity.Id)
                .HasForeignKey(transaction => transaction.SenderId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany<Transaction>()
                .WithOne()
                .HasPrincipalKey(transactionEntity => transactionEntity.Id)
                .HasForeignKey(transaction => transaction.RecipientId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasDiscriminator<string>("entity_type")
                .HasValue<TransactionSender>(nameof(TransactionSender))
                .HasValue<TransactionRecipient>(nameof(TransactionRecipient));
        }
    }
}
