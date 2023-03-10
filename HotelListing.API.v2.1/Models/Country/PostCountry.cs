namespace HotelListing.API.v2_1.Models.Country;

/// <summary>
///     Single Responsibility: Valid Data Transfer
/// </summary>
/// <remarks>
///     Prevent over posting attacks by removing the Id property.
///     Does not impact on data persistence model (entity framework).
/// </remarks>
public class PostCountry
{
    [Required] public string Name { get; set; }

    [Required] public string ShortName { get; set; }
}