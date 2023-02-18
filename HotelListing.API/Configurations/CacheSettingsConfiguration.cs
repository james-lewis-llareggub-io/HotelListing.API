namespace HotelListing.API.Configurations;

public class CacheSettingsConfiguration
{
    public CacheSettingsConfiguration(IConfiguration configuration)
    {
        MaximumSizeInMegabytes = Convert.ToInt32(configuration["Cache:MaximumSizeInMegabytes"]);
        MaximumAgeInSeconds = Convert.ToInt32(configuration["Cache:MaximumAgeInSeconds"]);
    }

    public int MaximumSizeInMegabytes { get; }

    public int MaximumAgeInSeconds { get; }
}