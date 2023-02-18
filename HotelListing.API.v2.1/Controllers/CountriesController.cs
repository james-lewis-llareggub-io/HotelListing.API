namespace HotelListing.API.v2_1.Controllers;

[ApiVersion("2.1")]
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