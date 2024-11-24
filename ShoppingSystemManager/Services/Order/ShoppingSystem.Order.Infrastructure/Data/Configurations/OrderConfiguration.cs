using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoppingSystem.Order.Core.Enums;
using ShoppingSystem.Order.Core.Models;
using ShoppingSystem.Order.Core.ValueObjects;

namespace ShoppingSystem.Order.Infrastructure.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Core.Models.Order>
    {
        public void Configure(EntityTypeBuilder<Core.Models.Order> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasConversion(x => x.Value, dbId => OrderId.Of(dbId));

            builder.HasOne<Customer>()
                .WithMany()
                .HasForeignKey(x => x.CustomerId)
                .IsRequired();

            builder.HasMany<OrderItem>()
                .WithOne()
                .HasForeignKey(x => x.OrderId);

            builder.OwnsOne(o => o.OrderName, nameBuilder =>
            {
                nameBuilder.Property(n => n.Value)
                    .HasColumnName(nameof(Core.Models.Order.OrderName))
                    .HasMaxLength(100)
                    .IsRequired();
            });

            // Configure ShippingAddress as an owned type
            builder.OwnsOne(o => o.ShippingAddress, addressBuilder =>
            {
                addressBuilder.Property(a => a.FirstName).HasMaxLength(50).IsRequired();
                addressBuilder.Property(a => a.LastName).HasMaxLength(50).IsRequired();
                addressBuilder.Property(a => a.EmailAddress).HasMaxLength(50);
                addressBuilder.Property(a => a.AddressLine).HasMaxLength(100).IsRequired();
                addressBuilder.Property(a => a.Country).HasMaxLength(50);
                addressBuilder.Property(a => a.State).HasMaxLength(50);
                addressBuilder.Property(a => a.ZipCode).HasMaxLength(50);
            });

            // Configure BillingAddress as an owned type
            builder.OwnsOne(o => o.BillingAddress, bAddressBuilder =>
            {
                bAddressBuilder.Property(a => a.FirstName).HasMaxLength(50).IsRequired();
                bAddressBuilder.Property(a => a.LastName).HasMaxLength(50).IsRequired();
                bAddressBuilder.Property(a => a.EmailAddress).HasMaxLength(50);
                bAddressBuilder.Property(a => a.AddressLine).HasMaxLength(100).IsRequired();
                bAddressBuilder.Property(a => a.Country).HasMaxLength(50);
                bAddressBuilder.Property(a => a.State).HasMaxLength(50);
                bAddressBuilder.Property(a => a.ZipCode).HasMaxLength(50);
            });

            // Configure Payment as an owned type
            builder.OwnsOne(o => o.Payment, paymentBuilder =>
            {
                paymentBuilder.Property(a => a.CardName).HasMaxLength(50);
                paymentBuilder.Property(a => a.CardNumber).HasMaxLength(24).IsRequired();
                paymentBuilder.Property(a => a.Expiration).HasMaxLength(10);
                paymentBuilder.Property(a => a.CVV).HasMaxLength(3);
                paymentBuilder.Property(a => a.PaymentMethod);
            });

            builder.Property(o => o.Status)
                .HasDefaultValue(OrderStatus.Draft)
                .HasConversion(s => s.ToString(), dbStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), dbStatus));

            builder.Property(o => o.TotalPrice);
        }
    }
}
