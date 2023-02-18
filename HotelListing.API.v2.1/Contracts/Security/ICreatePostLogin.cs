namespace HotelListing.API.v2_1.Contracts.Security;

public interface ICreatePostLogin
{
    Task<PostLogin> Create(IdentityUser user);
}