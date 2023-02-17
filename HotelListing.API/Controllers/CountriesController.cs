namespace HotelListing.API.Controllers;

public class CountriesController : AbstractController<
    Country,
    GetCountry,
    GetCountryDetail,
    PutCountry,
    PostCountry
>
{
    public CountriesController(ICountriesRepository repository, IMapper mapper) : base(repository, mapper)
    {
    }
}