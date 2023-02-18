namespace HotelListing.API.v2.Controllers;

[ApiVersion("2.0")]
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