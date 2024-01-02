using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Budgetify.Domain.Accounts;
using Budgetify.Domain.Accounts.SavingsAccounts;
using Budgetify.Domain.Accounts.TransactionalAccounts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Money.DB;

namespace Budgetify.Infrastructure.Configurations.AccountConfigurations
{
    internal sealed class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("accounts");

            builder.HasKey(account => account.Id);

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
                .HasForeignKey(transaction => transaction.Id);

            builder.HasDiscriminator<string>("account_type")
                .HasValue<SavingsAccount>(nameof(SavingsAccount))
                .HasValue<TransactionalAccount>(nameof(TransactionalAccount));
        }
    }
}
