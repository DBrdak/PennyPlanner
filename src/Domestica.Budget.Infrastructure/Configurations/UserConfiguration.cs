using Domestica.Budget.Domain.Accounts;
using Domestica.Budget.Domain.BudgetPlans;
using Domestica.Budget.Domain.TransactionCategories;
using Domestica.Budget.Domain.TransactionEntities;
using Domestica.Budget.Domain.Transactions;
using Domestica.Budget.Domain.TransactionSubcategories;
using Domestica.Budget.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Money.DB;

namespace Domestica.Budget.Infrastructure.Configurations
{
    internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            builder.HasKey(user => user.IdentityId);

            builder.Property(user => user.Id)
                .HasConversion(userId => userId.Value, value => new UserId(value));

            builder.Property(user => user.IdentityId)
                .HasConversion(userId => userId.Value, value => new UserIdentityId(value));

            builder.Property(user => user.Email)
                .HasMaxLength(400)
                .HasConversion(email => email.Value, value => new Domain.Users.Email(value));

            builder.Property(money => money.Currency)
                .HasConversion(currency => currency.Code, code => Currency.FromCode(code));

            builder.HasMany<Account>()
                .WithOne()
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany<BudgetPlan>()
                .WithOne()
                .HasForeignKey(bp => bp.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany<TransactionCategory>()
                .WithOne()
                .HasForeignKey(tc => tc.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany<Transaction>()
                .WithOne()
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany<TransactionSubcategory>()
                .WithOne()
                .HasForeignKey(tsc => tsc.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany<TransactionEntity>()
                .WithOne()
                .HasForeignKey(te => te.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(user => user.Email).IsUnique();

            builder.HasIndex(user => user.Id).IsUnique();
        }
    }
}
