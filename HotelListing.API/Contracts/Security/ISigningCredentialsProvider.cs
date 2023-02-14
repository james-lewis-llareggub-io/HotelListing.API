using Microsoft.IdentityModel.Tokens;

namespace HotelListing.API.Contracts.Security;

public interface ISigningCredentialsProvider
{
    SymmetricSecurityKey GetSymmetricSecurityKey();

    SigningCredentials GetCredentials();
}