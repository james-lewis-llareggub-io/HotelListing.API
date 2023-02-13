namespace HotelListing.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HotelsController : AbstractController<Hotel>
{
    public HotelsController(IHotelsRepository repository, IMapper mapper) : base(repository, mapper)
    {
    }

    // GET: api/Hotels
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetHotelDTO>>> Get()
    {
        var entities = await Repository.GetAllAsync();
        var dtos = Mapper.Map<List<GetHotelDTO>>(entities);
        return dtos;
    }

    // GET: api/Hotels/5
    [HttpGet("{id}")]
    public async Task<ActionResult<GetHotelDTO>> Get(int id)
    {
        var entity = await Repository.GetAsync(id);
        if (entity == null) return NotFound();
        var dto = Mapper.Map<GetHotelDTO>(entity);
        return Ok(dto);
    }

    // PUT: api/Hotels/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, GetHotelDTO dto)
    {
        if (id != dto.Id) return BadRequest();
        var entity = Mapper.Map<Hotel>(dto);
        var notFound = await Update(entity, id);
        if (notFound != null) return notFound;

        return NoContent();
    }

    // POST: api/Hotels
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Country>> Post(CreateHotelDTO dto)
    {
        var entity = Mapper.Map<Hotel>(dto);
        var result = await Repository.CreateAsync(entity);
        return CreatedAtAction("Get", new { id = result.Id }, result);
    }
}