using Microsoft.AspNetCore.Identity;

namespace HotelListing.API.Contracts.Security;

public interface ICreatePostLogin
{
    Task<PostLogin> Create(IdentityUser user);
}