using System.ComponentModel.DataAnnotations;

namespace HotelListing.API.Models.Hotel;

public class CreateHotelDTO
{
    [Required] public string Name { get; set; }

    [Required] public string Address { get; set; }

    [Required] public double Rating { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "The CountryId field is required.")]
    public int CountryId { get; set; }
}