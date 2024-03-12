using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Money.DB;
using PennyPlanner.Domain.Transactions;

namespace PennyPlanner.Infrastructure.Configurations
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

            builder.HasOne(transaction => transaction.Category)
                .WithMany()
                .HasPrincipalKey(category => category.Id)
                .HasForeignKey(transaction => transaction.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(transaction => transaction.Subcategory)
                .WithMany()
                .HasPrincipalKey(subcategory => subcategory.Id)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
