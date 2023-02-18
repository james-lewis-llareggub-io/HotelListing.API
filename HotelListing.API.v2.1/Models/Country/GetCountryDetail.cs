namespace HotelListing.API.v2_1.Models.Country;

public class GetCountryDetail : GetCountry
{
    public IList<GetHotel> Hotels { get; set; }
}