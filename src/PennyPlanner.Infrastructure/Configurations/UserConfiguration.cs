using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Money.DB;
using PennyPlanner.Domain.Accounts;
using PennyPlanner.Domain.BudgetPlans;
using PennyPlanner.Domain.TransactionCategories;
using PennyPlanner.Domain.TransactionEntities;
using PennyPlanner.Domain.Transactions;
using PennyPlanner.Domain.TransactionSubcategories;
using PennyPlanner.Domain.Users;

namespace PennyPlanner.Infrastructure.Configurations
{
    internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            builder.HasKey(user => user.Id);

            builder.Property(user => user.Id)
                .HasConversion(userId => userId.Value, value => new UserId(value));

            builder.Property(user => user.Username)
                .HasConversion(username => username.Value, value => new Username(value));

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
        }
    }
}
