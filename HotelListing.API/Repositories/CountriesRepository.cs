namespace HotelListing.API.Repositories;

public class CountriesRepository : Repository<Country>, ICountriesRepository 
{
    public CountriesRepository(HotelListingDbContext context) : base(context)
    {
        
    }

    public override async Task<Country?> GetAsync(int? id)
    {
        return await Context.Countries
             .Include(x => x.Hotels)
             .FirstOrDefaultAsync(x => x.Id == id);
    }
}