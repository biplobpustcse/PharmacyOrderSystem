namespace PharmacyOrderSystem.Application.DTOs;

public class CustomerDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string Address { get; set; } = null!;
}
