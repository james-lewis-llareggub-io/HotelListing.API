using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace HotelListing.API.Contracts.Security;

public interface IClaimsProvider
{
    Task<IList<Claim>> GetClaims(IdentityUser user);

    IEnumerable<Claim> GetJwtClaims(IdentityUser user);

    Task<IList<Claim>> GetRoleClaims(IdentityUser user);

    Task<IList<Claim>> GetUserClaims(IdentityUser user);
}