namespace PharmacyOrderSystem.Application.DTOs;

public class MedicineDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Category { get; set; } = null!;
    public decimal Price { get; set; }
    public int Stock { get; set; }
}
