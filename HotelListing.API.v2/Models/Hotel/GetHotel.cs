namespace HotelListing.API.v2.Models.Hotel;

public class GetHotel : IHaveAnId
{
    public string Name { get; set; }

    public string Address { get; set; }

    public double Rating { get; set; }

    public int CountryId { get; set; }
    public int Id { get; set; }
}