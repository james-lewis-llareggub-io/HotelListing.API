namespace HotelListing.API.Models.Country;

public class GetCountryDetailDTO
{
    public int Id { get; set; }
    
    public string Name { get; set; }

    public string ShortName { get; set; }
    
    public IList<GetHotelDTO> Hotels { get; set; }
}