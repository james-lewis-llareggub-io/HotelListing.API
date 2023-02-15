using HotelListing.API.Contracts.Security;
using HotelListing.API.Contracts.Security.Refresh;
using Microsoft.AspNetCore.Identity;

namespace HotelListing.API.Controllers.User;

[Route("api/[controller]")]
[ApiController]
public class RefreshController : ControllerBase
{
    private readonly IVerifyRefreshToken _verifyRefreshToken;
    
    public RefreshController(
        IVerifyRefreshToken verifyRefreshToken
    )
    {
        _verifyRefreshToken = verifyRefreshToken;
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] PostLogin dto)
    {
        var verified = await _verifyRefreshToken.Verify(dto);
        if (verified != null)
            return Ok(verified);
                   
        return Unauthorized();
    }
}