namespace HotelListing.API.v2_1.Models.Hotel;

public class PostHotel
{
    [Required] public string Name { get; set; }

    [Required] public string Address { get; set; }

    [Required] public double Rating { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "The CountryId field is required.")]
    public int CountryId { get; set; }
}