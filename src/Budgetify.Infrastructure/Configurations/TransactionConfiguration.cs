using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Budgetify.Domain.Transactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Money.DB;

namespace Budgetify.Infrastructure.Configurations
{
    internal sealed class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("transactions");

            builder.HasKey(t => t.Id);

            builder.Property(transaction => transaction.Id)
                .HasConversion(id => id.Value, value => new TransactionId(value));

            builder.OwnsOne(
                transaction => transaction.TransactionAmount,
                moneyBuilder =>
                {
                    moneyBuilder.Property(money => money.Currency)
                        .HasConversion(currency => currency.Code, code => Currency.FromCode(code));
                });

            builder.HasOne(transaction => transaction.Account)
                .WithMany(account => account.Transactions)
                .HasForeignKey(transaction => transaction.AccountId);

            builder.HasOne(transaction => transaction.Sender)
                .WithMany()
                .HasForeignKey(transaction => transaction.SenderId);

            builder.HasOne(transaction => transaction.FromAccount)
                .WithMany()
                .HasForeignKey(transaction => transaction.FromAccountId);

            builder.HasOne(transaction => transaction.Recipient)
                .WithMany()
                .HasForeignKey(transaction => transaction.RecipientId);

            builder.HasOne(transaction => transaction.ToAccount)
                .WithMany()
                .HasForeignKey(transaction => transaction.ToAccountId);

            builder.Property(transaction => transaction.Category)
                .HasConversion(category => category.Value, value => TransactionCategory.FromValue(value));
        }
    }
}
