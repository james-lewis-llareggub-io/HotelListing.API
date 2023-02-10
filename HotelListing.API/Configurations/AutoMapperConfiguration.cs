namespace HotelListing.API.Configurations;

public class AutoMapperConfiguration : Profile
{
    public AutoMapperConfiguration()
    {
        CreateMap<Country, CreateCountryDTO>().ReverseMap();
    }
}