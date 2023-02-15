using Microsoft.AspNetCore.Identity;

namespace HotelListing.API.Contracts.Security.Refresh;

public interface ICreateRefreshToken
{
    Task<string> Create(IdentityUser user);
}