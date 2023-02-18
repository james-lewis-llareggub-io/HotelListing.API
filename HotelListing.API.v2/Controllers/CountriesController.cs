namespace HotelListing.API.v2.Controllers;

[ApiVersion("2.0")]
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