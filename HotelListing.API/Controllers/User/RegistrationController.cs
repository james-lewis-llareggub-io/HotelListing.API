using Microsoft.AspNetCore.Identity;

namespace HotelListing.API.Controllers.User;

[Route("api/[controller]")]
[ApiController]
public class RegistrationController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly UserManager<IdentityUser> _userManager;

    public RegistrationController(IMapper mapper, UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] PostIdentityUser dto)
    {
        var user = _mapper.Map<IdentityUser>(dto);
        user.UserName = user.Email;
        var result = await _userManager.CreateAsync(user, dto.Password);

        if (!result.Errors.Any()) return Ok();

        foreach (var error in result.Errors) ModelState.AddModelError(error.Code, error.Description);
        return BadRequest(ModelState);
    }
}