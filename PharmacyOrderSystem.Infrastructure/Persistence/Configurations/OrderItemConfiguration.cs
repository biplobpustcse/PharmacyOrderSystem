using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PharmacyOrderSystem.Domain.Entities;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(oi => oi.Id);
        builder.Property(oi => oi.UnitPrice).HasColumnType("decimal(18,2)");

        builder.HasOne(oi => oi.Order)
               .WithMany(o => o.OrderItems)
               .HasForeignKey(oi => oi.OrderId);

        builder.HasOne(oi => oi.Medicine)
               .WithMany(m => m.OrderItems)
               .HasForeignKey(oi => oi.MedicineId);
    }
}
