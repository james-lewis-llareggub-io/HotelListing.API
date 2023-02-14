using HotelListing.API.Contracts.Security;
using Microsoft.AspNetCore.Identity;

namespace HotelListing.API.Controllers.User;

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IJwtSecurityTokenProvider _provider;
    private readonly UserManager<IdentityUser> _userManager;

    public LoginController(
        IMapper mapper,
        UserManager<IdentityUser> userManager,
        //IClaimsProvider provider
        IJwtSecurityTokenProvider provider
    )
    {
        _userManager = userManager;
        _provider = provider;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] PostIdentityUser dto)
    {
        var valid = false;
        try
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user != null)
            {
                valid = await _userManager.CheckPasswordAsync(user, dto.Password);
                if (valid)
                {
                    var token = new PostLogin
                    {
                        Token = await _provider.WriteJwtSecurityToken(user),
                        UserId = user.Id
                    };
                    return Ok(token);
                }
            }
        }
        catch (Exception)
        {
            // ignored
        }

        return Unauthorized();
    }
}