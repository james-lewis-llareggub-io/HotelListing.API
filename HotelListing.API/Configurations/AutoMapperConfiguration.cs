namespace HotelListing.API.Configurations;

public class AutoMapperConfiguration : Profile
{
    public AutoMapperConfiguration()
    {
        CreateMap<Country, PostCountry>().ReverseMap();
        CreateMap<Country, GetCountry>().ReverseMap();
        CreateMap<Country, GetCountryDetail>().ReverseMap();
        CreateMap<Country, PutCountry>().ReverseMap();
        CreateMap<Hotel, GetHotel>().ReverseMap();
        CreateMap<Hotel, PostHotel>().ReverseMap();
    }
}