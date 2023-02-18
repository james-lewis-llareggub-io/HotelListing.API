namespace HotelListing.API.v2.Models.Country;

public class GetCountryDetail : GetCountry
{
    public IList<GetHotel> Hotels { get; set; }
}