using Domestica.Budget.Domain.Accounts;
using Domestica.Budget.Domain.TransactionEntities;
using Domestica.Budget.Domain.Transactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Money.DB;

namespace Domestica.Budget.Infrastructure.Configurations
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

            builder.Property(transaction => transaction.Category)
                .HasConversion(category => category.Value, value => TransactionCategory.FromValue(value));

            builder.Property(transaction => transaction.SenderId)
                .HasConversion(id => id.Value, value => new TransactionEntityId(value));
            builder.Property(transaction => transaction.RecipientId)
                .HasConversion(id => id.Value, value => new TransactionEntityId(value));
            builder.Property(transaction => transaction.FromAccountId)
                .HasConversion(id => id.Value, value => new AccountId(value));
            builder.Property(transaction => transaction.ToAccountId)
                .HasConversion(id => id.Value, value => new AccountId(value));
        }
    }
}
