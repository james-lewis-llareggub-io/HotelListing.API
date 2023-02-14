using HotelListing.API.Contracts.Security;

namespace HotelListing.API.Security;

public class JwtSettingsConfiguration : IJwtSettingsConfiguration
{
    public JwtSettingsConfiguration(IConfiguration configuration)
    {
        Issuer = configuration["JwtSettings:Issuer"];
        Audience = configuration["JwtSettings:Audience"];
        DurationInMinutes = Convert.ToInt32(configuration["JwtSettings:DurationInMinutes"]);
    }

    public string Issuer { get; }
    public string Audience { get; }
    public int DurationInMinutes { get; }
}