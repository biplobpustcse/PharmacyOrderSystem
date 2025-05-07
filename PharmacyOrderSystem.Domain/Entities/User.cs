namespace PharmacyOrderSystem.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Role { get; set; } = "Staff";  // Default role is Staff, but it can be Admin
}
