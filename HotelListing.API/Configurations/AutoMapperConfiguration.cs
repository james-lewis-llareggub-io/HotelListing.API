namespace HotelListing.API.Configurations;

public class AutoMapperConfiguration : Profile
{
    public AutoMapperConfiguration()
    {
        CreateMap<Country, CreateCountryDTO>().ReverseMap();
        CreateMap<Country, GetCountryDTO>().ReverseMap();
        CreateMap<Country, GetCountryDetailDTO>().ReverseMap();
        CreateMap<Country, UpdateCountryDTO>().ReverseMap();
        CreateMap<Hotel, GetHotelDTO>().ReverseMap();
        CreateMap<Hotel, CreateHotelDTO>().ReverseMap();
    }
}