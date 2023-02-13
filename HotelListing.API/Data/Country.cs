namespace HotelListing.API.Data;

public class Country : IHaveAnId
{
    public string Name { get; set; }

    public string ShortName { get; set; }

    public virtual IList<Hotel>? Hotels { get; set; }
    public int Id { get; set; }
}