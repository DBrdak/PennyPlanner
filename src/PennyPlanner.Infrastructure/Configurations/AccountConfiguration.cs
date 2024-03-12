using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Money.DB;
using PennyPlanner.Domain.Accounts;
using PennyPlanner.Domain.Accounts.SavingsAccounts;
using PennyPlanner.Domain.Accounts.TransactionalAccounts;
using PennyPlanner.Domain.Transactions;

namespace PennyPlanner.Infrastructure.Configurations
{
    internal sealed class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("accounts");

            builder.HasKey(account => account.Id);

            builder.Property(account => account.Id)
                .HasConversion(id => id.Value, value => new AccountId(value));

            builder.Property(account => account.Name)
                .HasConversion(accountName => accountName.Value, value => new AccountName(value));

            builder.OwnsOne(
                account => account.Balance,
                moneyBuilder =>
                {
                    moneyBuilder.Property(money => money.Currency)
                        .HasConversion(currency => currency.Code, code => Currency.FromCode(code));
                });

            builder.HasMany(account => account.Transactions)
                .WithOne()
                .HasPrincipalKey(account => account.Id)
                .HasForeignKey(transaction => transaction.AccountId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany<Transaction>()
                .WithOne()
                .HasPrincipalKey(account => account.Id)
                .HasForeignKey(transaction => transaction.FromAccountId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany<Transaction>()
                .WithOne()
                .HasPrincipalKey(account => account.Id)
                .HasForeignKey(transaction => transaction.ToAccountId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasDiscriminator<string>("account_type")
                .HasValue<SavingsAccount>(nameof(SavingsAccount))
                .HasValue<TransactionalAccount>(nameof(TransactionalAccount));
        }
    }
}
