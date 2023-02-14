using System.IdentityModel.Tokens.Jwt;
using HotelListing.API.Contracts.Security;
using Microsoft.AspNetCore.Identity;

namespace HotelListing.API.Security;

public class JwtSecurityTokenProvider : IJwtSecurityTokenProvider
{
    private readonly IClaimsProvider _claimsProvider;
    private readonly IConfiguration _configuration;
    private readonly IJwtSettingsConfiguration _jwtSettingsConfiguration;
    private readonly ISigningCredentialsProvider _signingCredentialsProvider;

    public JwtSecurityTokenProvider(
        IConfiguration configuration,
        ISigningCredentialsProvider signingCredentialsProvider,
        IClaimsProvider claimsProvider,
        IJwtSettingsConfiguration jwtSettingsConfiguration
    )
    {
        _configuration = configuration;
        _signingCredentialsProvider = signingCredentialsProvider;
        _claimsProvider = claimsProvider;
        _jwtSettingsConfiguration = jwtSettingsConfiguration;
    }

    public async Task<JwtSecurityToken> GetJwtSecurityToken(IdentityUser user)
    {
        var signingCredentials = _signingCredentialsProvider.GetCredentials();
        var claims = await _claimsProvider.GetClaims(user);
        var token = new JwtSecurityToken(
            _jwtSettingsConfiguration.Issuer,
            _jwtSettingsConfiguration.Audience,
            claims,
            expires: DateTime.Now.AddMinutes(_jwtSettingsConfiguration.DurationInMinutes),
            signingCredentials: signingCredentials
        );

        return token;
    }

    public async Task<string> WriteJwtSecurityToken(IdentityUser user)
    {
        var token = await GetJwtSecurityToken(user);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}