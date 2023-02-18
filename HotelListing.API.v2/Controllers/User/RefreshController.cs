namespace HotelListing.API.v2.Controllers.User;

[ApiVersion("2.0")]
public class RefreshController : AbstractUserController
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