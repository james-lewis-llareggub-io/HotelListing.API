namespace HotelListing.API.v2_1.Contracts.Security;

public interface IJwtSecurityTokenProvider
{
    Task<JwtSecurityToken> GetJwtSecurityToken(IdentityUser user);

    Task<string> WriteJwtSecurityToken(IdentityUser user);
}