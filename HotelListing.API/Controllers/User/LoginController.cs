using Microsoft.AspNetCore.Identity;

namespace HotelListing.API.Controllers.User;

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly UserManager<IdentityUser> _userManager;

    public LoginController(IMapper mapper, UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] PostIdentityUser dto)
    {
        bool valid = false;
        try
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user != null)
                valid = await _userManager.CheckPasswordAsync(user, dto.Password);

            if (valid)
                return Ok();            
        }
        catch (Exception)
        {
            // ignored
        }

        return Unauthorized();
    }
}