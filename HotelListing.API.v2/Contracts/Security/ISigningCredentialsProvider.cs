using Microsoft.IdentityModel.Tokens;

namespace HotelListing.API.v2.Contracts.Security;

public interface ISigningCredentialsProvider
{
    SymmetricSecurityKey GetSymmetricSecurityKey();

    SigningCredentials GetCredentials();
}