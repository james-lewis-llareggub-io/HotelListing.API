namespace HotelListing.API.Models.Country;

public class GetCountryDetailDTO : GetCountryDTO
{
    public IList<GetHotelDTO> Hotels { get; set; }
}