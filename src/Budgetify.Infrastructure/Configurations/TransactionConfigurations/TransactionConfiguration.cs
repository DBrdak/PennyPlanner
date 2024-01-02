using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Budgetify.Domain.Transactions;
using Budgetify.Domain.Transactions.IncomingTransactions;
using Budgetify.Domain.Transactions.OugoingTransactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Money.DB;

namespace Budgetify.Infrastructure.Configurations.TransactionConfigurations
{
    internal sealed class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("transactions");

            builder.HasKey(t => t.Id);

            builder.OwnsOne(
                transaction => transaction.TransactionAmount,
                moneyBuilder =>
                {
                    moneyBuilder.Property(money => money.Currency)
                        .HasConversion(currency => currency.Code, code => Currency.FromCode(code));
                });

            builder.HasDiscriminator<string>("transaction_type")
                .HasValue<IncomingTransaction>(nameof(IncomingTransaction))
                .HasValue<OutgoingTransaction>(nameof(OutgoingTransaction));

        }
    }
}
