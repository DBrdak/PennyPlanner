using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PennyPlanner.Domain.TransactionCategories;
using PennyPlanner.Domain.Transactions;

namespace PennyPlanner.Infrastructure.Configurations
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
