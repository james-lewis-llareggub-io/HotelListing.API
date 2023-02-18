namespace HotelListing.API.v2_1.Controllers;

[ApiVersion("2.1")]
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