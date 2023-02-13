namespace HotelListing.API.Models.Country;

public class GetCountryDetail : GetCountry
{
    public IList<GetHotel> Hotels { get; set; }
}