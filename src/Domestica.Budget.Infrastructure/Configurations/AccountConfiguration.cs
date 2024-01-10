using Domestica.Budget.Domain.Accounts;
using Domestica.Budget.Domain.Accounts.SavingsAccounts;
using Domestica.Budget.Domain.Accounts.TransactionalAccounts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Money.DB;

namespace Domestica.Budget.Infrastructure.Configurations
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
                .HasForeignKey(transaction => transaction.Id);

            builder.HasDiscriminator<string>("account_type")
                .HasValue<SavingsAccount>(nameof(SavingsAccount))
                .HasValue<TransactionalAccount>(nameof(TransactionalAccount));
        }
    }
}
