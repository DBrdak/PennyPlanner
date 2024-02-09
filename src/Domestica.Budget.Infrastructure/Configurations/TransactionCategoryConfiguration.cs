using Domestica.Budget.Domain.Accounts.SavingsAccounts;
using Domestica.Budget.Domain.Accounts.TransactionalAccounts;
using Domestica.Budget.Domain.TransactionCategories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domestica.Budget.Infrastructure.Configurations
{
    internal sealed class TransactionCategoryConfiguration : IEntityTypeConfiguration<TransactionCategory>
    {
        public void Configure(EntityTypeBuilder<TransactionCategory> builder)
        {
            builder.ToTable("transaction_categories");

            builder.HasKey(transactionCategory => transactionCategory.Id);

            builder.Property(transactionCategory => transactionCategory.Id)
                .HasConversion(id => id.Value, value => new TransactionCategoryId(value));

            builder.Property(transactionCategory => transactionCategory.Value)
                .HasConversion(value => value.Value, value => new TransactionCategoryValue(value));

            builder.HasDiscriminator<string>("transaction_category_type")
                .HasValue<IncomeTransactionCategory>(nameof(IncomeTransactionCategory))
                .HasValue<OutcomeTransactionCategory>(nameof(OutcomeTransactionCategory));
        }
    }
}
