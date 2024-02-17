using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domestica.Budget.Domain.TransactionCategories;
using Domestica.Budget.Domain.TransactionSubcategories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domestica.Budget.Infrastructure.Configurations
{
    internal sealed class TransactionSubcategoryConfiguration : IEntityTypeConfiguration<TransactionSubcategory>
    {
        public void Configure(EntityTypeBuilder<TransactionSubcategory> builder)
        {
            builder.ToTable("transaction_subcategories");

            builder.HasKey(transactionSubcategory => transactionSubcategory.Id);

            builder.Property(transactionSubcategory => transactionSubcategory.Id)
                .HasConversion(id => id.Value, value => new TransactionSubcategoryId(value));

            builder.Property(transactionCategory => transactionCategory.Value)
                .HasConversion(value => value.Value, value => new TransactionSubcategoryValue(value));

            builder.HasOne(subcategory => subcategory.Category)
                .WithMany(category => category.Subcategories)
                .HasForeignKey(subcategory => subcategory.CategoryId) 
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
