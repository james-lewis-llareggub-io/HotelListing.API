namespace HotelListing.API.Controllers;

public class HotelsController : AbstractController<
    Hotel,
    GetHotel,
    GetHotel,
    GetHotel,
    PostHotel
>
{
    public HotelsController(IHotelsRepository repository, IMapper mapper) : base(repository, mapper)
    {
    }
}