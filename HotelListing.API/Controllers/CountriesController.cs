namespace HotelListing.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CountriesController : AbstractController<
    Country,
    GetCountryDTO,
    GetCountryDetailDTO,
    UpdateCountryDTO,
    CreateCountryDTO
>
{
    public CountriesController(ICountriesRepository repository, IMapper mapper) : base(repository, mapper)
    {
    }
}