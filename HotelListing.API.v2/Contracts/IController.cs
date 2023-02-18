namespace HotelListing.API.v2.Contracts;

public interface IController<T, TGet, TGetDetail, in TUpdate, in TCreate>
    where T : class, IHaveAnId
    where TGet : class
    where TGetDetail : class
    where TUpdate : class, IHaveAnId
    where TCreate : class
{
    // GET: api/Countries
    Task<ActionResult<IEnumerable<TGet>>> GetAll();

    // GET: api/Countries/5
    Task<ActionResult<TGetDetail>> Get(int id);

    // POST: api/Countries
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    Task<ActionResult<TGet>> Post(TCreate dto);

    // PUT: api/Countries/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    Task<IActionResult> Put(int id, TUpdate dto);

    // DELETE: api/Countries/5
    Task<IActionResult> Delete(int id);
}