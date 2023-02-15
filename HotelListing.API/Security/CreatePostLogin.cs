using HotelListing.API.Contracts.Security;
using HotelListing.API.Contracts.Security.Refresh;
using Microsoft.AspNetCore.Identity;

namespace HotelListing.API.Security;

public class CreatePostLogin : ICreatePostLogin
{
    private readonly IJwtSecurityTokenProvider _provider;
    private readonly ICreateRefreshToken _createRefreshToken;

    public CreatePostLogin(
        IJwtSecurityTokenProvider provider,
        ICreateRefreshToken createRefreshToken
    )
    {
        _provider = provider;
        _createRefreshToken = createRefreshToken;
    }
    
    public async Task<PostLogin> Create(IdentityUser user)
    {
        var token = await _provider.WriteJwtSecurityToken(user);
        var refreshToken = await _createRefreshToken.Create(user);
        var dto = new PostLogin
        {
            Token = token,
            UserId = user.Id,
            RefreshToken = refreshToken 
        };
        return dto;
    }
}