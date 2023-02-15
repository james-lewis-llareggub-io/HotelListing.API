using System.IdentityModel.Tokens.Jwt;
using HotelListing.API.Contracts.Security;
using HotelListing.API.Contracts.Security.Refresh;
using Microsoft.AspNetCore.Identity;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace HotelListing.API.Security.Refresh;

public class VerifyRefreshToken : IVerifyRefreshToken
{
    private readonly ICreatePostLogin _createPostLogin;
    private readonly UserManager<IdentityUser> _userManager;

    public VerifyRefreshToken(
        UserManager<IdentityUser> userManager,
        ICreatePostLogin createPostLogin
    )
    {
        _userManager = userManager;
        _createPostLogin = createPostLogin;
    }

    public async Task<PostLogin?> Verify(PostLogin dto)
    {
        var user = await GetIdentityUser(dto);
        var verified = await IsVerified(dto, user);
        if (verified == false)
            return null;
        return await _createPostLogin.Create(user);
    }

    private async Task<IdentityUser> GetIdentityUser(PostLogin dto)
    {
        var handler = new JwtSecurityTokenHandler();
        var content = handler.ReadJwtToken(dto.Token);
        var username = content.Claims.ToList()
            .FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub)?.Value;
        var user = await _userManager.FindByIdAsync(dto.UserId);
        if (user.UserName != username)
            throw new UnauthorizedAccessException();
        return user;
    }

    private async Task<bool> IsVerified(PostLogin dto, IdentityUser user)
    {
        var verified = await _userManager.VerifyUserTokenAsync(
            user,
            CreateRefreshToken.LoginProvider,
            CreateRefreshToken.RefreshTokenName,
            dto.RefreshToken
        );
        if (verified == false)
            await _userManager.UpdateSecurityStampAsync(user);

        return verified;
    }
}