using PharmacyOrderSystem.Domain.Enums;

namespace PharmacyOrderSystem.Domain.Entities;

public class Order
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public OrderStatus Status { get; set; } = OrderStatus.Pending;

    public int CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;

    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    // Flat discount on total (optional)
    public decimal DiscountAmount { get; set; } = 0;

    // Total calculation
    public decimal SubTotal => OrderItems.Sum(item => item.Quantity * item.UnitPrice);
    public decimal TotalPrice => SubTotal - DiscountAmount;
}
