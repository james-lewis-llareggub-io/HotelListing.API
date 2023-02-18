namespace HotelListing.API.v2_1.Contracts.Security.Refresh;

public interface ICreateRefreshToken
{
    Task<string> Create(IdentityUser user);
}