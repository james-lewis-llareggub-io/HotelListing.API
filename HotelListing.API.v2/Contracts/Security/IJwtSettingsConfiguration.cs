namespace HotelListing.API.v2.Contracts.Security;

public interface IJwtSettingsConfiguration
{
    string Issuer { get; }
    string Audience { get; }
    int DurationInMinutes { get; }
}