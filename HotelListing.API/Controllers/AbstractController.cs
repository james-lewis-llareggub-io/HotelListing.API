namespace HotelListing.API.Controllers;

public abstract class AbstractController<T> : ControllerBase where T : class
{
    protected readonly IMapper Mapper;
    protected readonly IRepository<T> Repository;

    protected AbstractController(IRepository<T> repository, IMapper mapper)
    {
        Repository = repository;
        Mapper = mapper;
    }

    private async Task<bool> Exists(int id)
    {
        return await Repository.Exists(id);
    }

    protected async Task<NotFoundResult?> Update(T entity, int id)
    {
        try
        {
            await Repository.UpdateAsync(entity);
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

    // DELETE: api/Countries/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await Repository.DeleteAsync(id);
        return NoContent();
    }
}