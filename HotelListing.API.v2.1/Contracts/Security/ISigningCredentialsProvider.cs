namespace HotelListing.API.v2_1.Contracts.Security;

public interface ISigningCredentialsProvider
{
    SymmetricSecurityKey GetSymmetricSecurityKey();

    SigningCredentials GetCredentials();
}