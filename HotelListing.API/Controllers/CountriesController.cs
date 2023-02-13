namespace HotelListing.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CountriesController : AbstractController<Country>
{
    public CountriesController(ICountriesRepository repository, IMapper mapper) : base(repository, mapper)
    {
    }

    // GET: api/Countries
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetCountryDTO>>> GetCountries()
    {
        var countries = await Repository.GetAllAsync();
        var list = Mapper.Map<List<GetCountryDTO>>(countries);
        return list;
    }

    // GET: api/Countries/5
    [HttpGet("{id}")]
    public async Task<ActionResult<GetCountryDetailDTO>> GetCountry(int id)
    {
        var country = await Repository.GetAsync(id);
        if (country == null) return NotFound();
        var dto = Mapper.Map<GetCountryDetailDTO>(country);
        return Ok(dto);
    }

    // PUT: api/Countries/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCountry(int id, UpdateCountryDTO dto)
    {
        if (id != dto.Id) return BadRequest();
        var country = Mapper.Map<Country>(dto);
        var notFound = await Update(country, id);
        if (notFound != null) return notFound;

        return NoContent();
    }

    // POST: api/Countries
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Country>> PostCountry(CreateCountryDTO dto)
    {
        var country = Mapper.Map<Country>(dto);
        var result = await Repository.CreateAsync(country);
        return CreatedAtAction("GetCountry", new { id = result.Id }, result);
    }
}