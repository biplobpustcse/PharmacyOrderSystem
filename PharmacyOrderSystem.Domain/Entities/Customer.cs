namespace PharmacyOrderSystem.Domain.Entities;

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string Address { get; set; } = null!;

    public ICollection<Order> Orders { get; set; } = new List<Order>();
}
