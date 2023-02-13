namespace HotelListing.API.Repositories;

public class HotelsRepository : Repository<Hotel>, IHotelsRepository
{
    public HotelsRepository(HotelListingDbContext context) : base(context)
    {
    }
}