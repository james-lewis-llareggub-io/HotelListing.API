namespace HotelListing.API.Contracts.Security;

public interface IJwtSecurityTokenProvider
{
    Task<JwtSecurityToken> GetJwtSecurityToken(IdentityUser user);

    Task<string> WriteJwtSecurityToken(IdentityUser user);
}