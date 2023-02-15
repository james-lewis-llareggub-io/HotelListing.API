using HotelListing.API.Contracts.Security.Refresh;
using Microsoft.AspNetCore.Identity;

namespace HotelListing.API.Security.Refresh;

public class CreateRefreshToken : ICreateRefreshToken
{
    private readonly UserManager<IdentityUser> _userManager;

    public const string LoginProvider = "HotelListingAPI";

    public const string RefreshTokenName = "RefreshToken";
        
    public CreateRefreshToken(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<string> Create(IdentityUser user)
    {
        await _userManager.RemoveAuthenticationTokenAsync(user, LoginProvider, RefreshTokenName);
        var token = await _userManager.GenerateUserTokenAsync(user, LoginProvider, RefreshTokenName);
        return token;
    }
}