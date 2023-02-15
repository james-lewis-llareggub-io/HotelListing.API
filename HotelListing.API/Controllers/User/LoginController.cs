using HotelListing.API.Contracts.Security;
using HotelListing.API.Contracts.Security.Refresh;
using Microsoft.AspNetCore.Identity;

namespace HotelListing.API.Controllers.User;

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly ICreatePostLogin _createPostLogin;
    private readonly ICreateRefreshToken _createRefreshToken;
    private readonly IJwtSecurityTokenProvider _provider;
    private readonly UserManager<IdentityUser> _userManager;

    public LoginController(
        UserManager<IdentityUser> userManager,
        IJwtSecurityTokenProvider provider,
        ICreateRefreshToken createRefreshToken,
        ICreatePostLogin createPostLogin
    )
    {
        _userManager = userManager;
        _provider = provider;
        _createRefreshToken = createRefreshToken;
        _createPostLogin = createPostLogin;
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
                    var token = await _createPostLogin.Create(user);
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