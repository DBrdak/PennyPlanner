using Domestica.Budget.Domain.BudgetPlans;
using Domestica.Budget.Domain.Transactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Money.DB;

namespace Domestica.Budget.Infrastructure.Configurations
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
                    budgetedTransactionCategoryBuilder
                        .Property(budgetedTransactionCategory => budgetedTransactionCategory.Category).HasConversion(
                            category => category.Value,
                            value => TransactionCategory.FromValue(value));
                });

            builder.HasMany(budgetPlan => budgetPlan.Transactions)
                .WithOne()
                .HasPrincipalKey(budgetPlan => budgetPlan.Id);
        }
    }
}
