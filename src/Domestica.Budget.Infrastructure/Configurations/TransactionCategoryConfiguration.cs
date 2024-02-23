using Domestica.Budget.Domain.Accounts.SavingsAccounts;
using Domestica.Budget.Domain.Accounts.TransactionalAccounts;
using Domestica.Budget.Domain.TransactionCategories;
using Domestica.Budget.Domain.Transactions;
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

            builder.HasMany<Transaction>()
                .WithOne(t => t.Category)
                .HasPrincipalKey(transactionCategory => transactionCategory.Id)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(category => category.Subcategories)
                .WithOne(subcategory => subcategory.Category)
                .HasForeignKey(subcategory => subcategory.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasDiscriminator<string>("transaction_category_type")
                .HasValue<IncomeTransactionCategory>(nameof(IncomeTransactionCategory))
                .HasValue<OutcomeTransactionCategory>(nameof(OutcomeTransactionCategory));
        }
    }
}
