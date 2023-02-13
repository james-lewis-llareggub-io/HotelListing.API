namespace HotelListing.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HotelsController : AbstractController<
    Hotel, 
    GetHotelDTO,
    GetHotelDTO,
    GetHotelDTO,
    CreateHotelDTO
>
{
    public HotelsController(IHotelsRepository repository, IMapper mapper) : base(repository, mapper)
    {
    }
}