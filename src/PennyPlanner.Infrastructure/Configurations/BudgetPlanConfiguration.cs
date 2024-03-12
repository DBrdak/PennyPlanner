using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Money.DB;
using PennyPlanner.Domain.BudgetPlans;

namespace PennyPlanner.Infrastructure.Configurations
{
    internal sealed class BudgetPlanConfiguration : IEntityTypeConfiguration<BudgetPlan>
    {
        public void Configure(EntityTypeBuilder<BudgetPlan> builder)
        {
            builder.ToTable("budget_plans");

            builder.HasKey(x => x.Id);

            builder.Property(budgetPlan => budgetPlan.Id)
                .HasConversion(id => id.Value, value => new BudgetPlanId(value));

            builder.OwnsOne(budgetPlan => budgetPlan.BudgetPeriod);

            builder.OwnsMany(budgetPlan => budgetPlan.BudgetedTransactionCategories,
                budgetedTransactionCategoryBuilder =>
                {
                    budgetedTransactionCategoryBuilder.OwnsOne(
                        budgetedTransactionCategory => budgetedTransactionCategory.ActualAmount,
                        moneyBuilder =>
                        {
                            moneyBuilder.Property(money => money.Currency).HasConversion(currency => currency.Code, code => Currency.FromCode(code));
                        });
                    budgetedTransactionCategoryBuilder.OwnsOne(
                        budgetedTransactionCategory => budgetedTransactionCategory.BudgetedAmount,
                        moneyBuilder =>
                        {
                            moneyBuilder.Property(money => money.Currency).HasConversion(currency => currency.Code, code => Currency.FromCode(code));
                        });
                    budgetedTransactionCategoryBuilder.HasOne(budgetedTransactionCategory => budgetedTransactionCategory.Category)
                        .WithMany()
                        .HasForeignKey(budgetedTransactionCategory => budgetedTransactionCategory.CategoryId);
                });

            builder.HasMany(budgetPlan => budgetPlan.Transactions)
                .WithOne()
                .HasPrincipalKey(budgetPlan => budgetPlan.Id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
