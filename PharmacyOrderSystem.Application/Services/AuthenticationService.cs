using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PharmacyOrderSystem.Application.Interfaces;
using PharmacyOrderSystem.Application.Settings;
using PharmacyOrderSystem.Domain.Entities;

namespace PharmacyOrderSystem.Application.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly JWTSettings _jwtSettings;
    private readonly List<User> _users;

    public AuthenticationService(IOptions<JWTSettings> jwtSettingsOption)
    {
        _jwtSettings = jwtSettingsOption.Value;
        // Simulate a user database or fetch from DB
        _users = new List<User>
        {
            new User
            {
                Id = 1,
                Username = "admin",
                Password = "adminpassword",
                Role = "Admin",
            },
            new User
            {
                Id = 2,
                Username = "staff",
                Password = "staffpassword",
                Role = "Staff",
            },
        };
    }

    public async Task<string> AuthenticateAsync(string username, string password)
    {
        var user = _users.FirstOrDefault(u => u.Username == username && u.Password == password);
        if (user == null)
        {
            throw new UnauthorizedAccessException("Invalid credentials.");
        }

        var token = GenerateJwtToken(user);
        return await Task.FromResult(token);
    }

    private string GenerateJwtToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role),
        };

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(_jwtSettings.ExpiryDurationInMinutes),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
