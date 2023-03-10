namespace HotelListing.API.v2.Controllers;

[Route("api/v2/[controller]")]
[ApiController]
public abstract class AbstractController<T, TGet, TGetDetail, TUpdate, TCreate> :
    ControllerBase,
    IController<T, TGet, TGetDetail, TUpdate, TCreate>
    where T : class, IHaveAnId
    where TGet : class
    where TGetDetail : class
    where TUpdate : class, IHaveAnId
    where TCreate : class
{
    private readonly IMapper _mapper;
    private readonly IRepository<T> _repository;

    protected AbstractController(IRepository<T> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    // GET: api/Countries
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TGet>>> GetAll()
    {
        var entities = await _repository.GetAllAsync();
        var list = _mapper.Map<List<TGet>>(entities);
        return list;
    }

    // GET: api/Countries/5
    [HttpGet("{id}")]
    public async Task<ActionResult<TGetDetail>> Get(int id)
    {
        var entity = await _repository.GetAsync(id);
        if (entity == null) return NotFound();
        var dto = _mapper.Map<TGetDetail>(entity);
        return Ok(dto);
    }

    // POST: api/Countries
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<TGet>> Post(TCreate dto)
    {
        var entity = _mapper.Map<T>(dto);
        var result = await _repository.CreateAsync(entity);
        return CreatedAtAction("Get", new { id = result.Id }, result);
    }

    // PUT: api/Countries/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> Put(int id, TUpdate dto)
    {
        if (id != dto.Id) return BadRequest();
        var entity = _mapper.Map<T>(dto);
        var notFound = await Update(entity, id);
        if (notFound != null) return notFound;

        return NoContent();
    }

    // DELETE: api/Countries/5
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> Delete(int id)
    {
        await _repository.DeleteAsync(id);
        return NoContent();
    }

    private async Task<bool> Exists(int id)
    {
        return await _repository.Exists(id);
    }

    private async Task<NotFoundResult?> Update(T entity, int id)
    {
        try
        {
            await _repository.UpdateAsync(entity);
            return null;
        }
        catch (DbUpdateConcurrencyException)
        {
            var exists = await Exists(id);
            if (!exists)
                return NotFound();
            throw;
        }
    }
}