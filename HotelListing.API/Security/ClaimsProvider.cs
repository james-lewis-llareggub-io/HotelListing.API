using System.Security.Claims;
using HotelListing.API.Contracts.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.JsonWebTokens;

namespace HotelListing.API.Security;

public class ClaimsProvider : IClaimsProvider
{
    private readonly UserManager<IdentityUser> _manager;

    public ClaimsProvider(UserManager<IdentityUser> manager)
    {
        _manager = manager;
    }

    public async Task<IList<Claim>> GetClaims(IdentityUser user)
    {
        var claims = GetJwtClaims(user)
            .Union(await GetRoleClaims(user))
            .Union(await GetUserClaims(user));

        return claims.ToList();
    }

    public IEnumerable<Claim> GetJwtClaims(IdentityUser user)
    {
        List<Claim> claims = new()
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email)
        };

        return claims;
    }

    public async Task<IList<Claim>> GetRoleClaims(IdentityUser user)
    {
        var roles = await _manager.GetRolesAsync(user);
        var claims = roles.Select(x => new Claim(ClaimTypes.Role, x)).ToList();
        return claims;
    }

    public async Task<IList<Claim>> GetUserClaims(IdentityUser user)
    {
        var claims = await _manager.GetClaimsAsync(user);
        return claims;
    }
}