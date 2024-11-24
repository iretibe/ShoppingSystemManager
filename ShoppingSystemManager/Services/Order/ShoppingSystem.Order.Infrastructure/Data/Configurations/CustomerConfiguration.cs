using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoppingSystem.Order.Core.Models;
using ShoppingSystem.Order.Core.ValueObjects;

namespace ShoppingSystem.Order.Infrastructure.Data.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                .HasConversion(c => c.Value, dbId => CustomerId.Of(dbId));

            builder.Property(c => c.Name).HasMaxLength(100).IsRequired();

            builder.Property(c => c.Email).HasMaxLength(255).IsRequired();
            builder.HasIndex(c => c.Email).IsUnique();
        }
    }
}
