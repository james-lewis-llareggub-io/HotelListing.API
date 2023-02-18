namespace HotelListing.API.v2.Contracts.Security.Refresh;

public interface IVerifyRefreshToken
{
    Task<PostLogin?> Verify(PostLogin dto);
}