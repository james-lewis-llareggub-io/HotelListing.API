namespace HotelListing.API.Contracts.Security;

public interface IJwtSettingsConfiguration
{
    string Issuer { get; }
    string Audience { get; }
    int DurationInMinutes { get; }
}