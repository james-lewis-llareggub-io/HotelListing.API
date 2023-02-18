namespace HotelListing.API.v2.Contracts.Security.Refresh;

public interface ICreateRefreshToken
{
    Task<string> Create(IdentityUser user);
}