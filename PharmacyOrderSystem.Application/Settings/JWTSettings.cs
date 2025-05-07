namespace PharmacyOrderSystem.Application.Settings;

public class JWTSettings
{
    public string SecretKey { get; set; } = string.Empty;
    public double ExpiryDurationInMinutes { get; set; }
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
}
