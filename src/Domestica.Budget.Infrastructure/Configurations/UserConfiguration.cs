using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domestica.Budget.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domestica.Budget.Infrastructure.Configurations
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
                .HasMaxLength(30)
                .HasConversion(firstName => firstName.Value, value => new Username(value));

            builder.Property(user => user.Email)
                .HasMaxLength(400)
                .HasConversion(email => email.Value, value => new Domain.Users.Email(value));

            builder.HasIndex(user => user.Email).IsUnique();

            builder.HasIndex(user => user.IdentityId).IsUnique();
        }
    }
}
