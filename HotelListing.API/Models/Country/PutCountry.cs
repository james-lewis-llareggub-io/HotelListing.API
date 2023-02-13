namespace HotelListing.API.Models.Country;

/// <summary>
///     Single Responsibility: Valid Data Transfer
/// </summary>
/// <remarks>
///     Prevent over posting attacks by removing the Id property.
///     Does not impact on data persistence model (entity framework).
/// </remarks>
public class PutCountry : PostCountry, IHaveAnId
{
    [Required] public int Id { get; set; }
}