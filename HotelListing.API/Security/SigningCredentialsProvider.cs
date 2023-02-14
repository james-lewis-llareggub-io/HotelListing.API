using System.Text;
using HotelListing.API.Contracts.Security;
using Microsoft.IdentityModel.Tokens;

namespace HotelListing.API.Security;

public class SigningCredentialsProvider : ISigningCredentialsProvider
{
    private const string SecurityKey = "JwtSettings:Secrets:Key";
    private readonly IConfiguration _configuration;

    public SigningCredentialsProvider(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        var securityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration[SecurityKey] ?? string.Empty)
        );
        return securityKey;
    }

    public SigningCredentials GetCredentials()
    {
        var securityKey = GetSymmetricSecurityKey();
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        return credentials;
    }
}