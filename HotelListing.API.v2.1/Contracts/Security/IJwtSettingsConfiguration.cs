namespace HotelListing.API.v2_1.Contracts.Security;

public interface IJwtSettingsConfiguration
{
    string Issuer { get; }
    string Audience { get; }
    int DurationInMinutes { get; }
}