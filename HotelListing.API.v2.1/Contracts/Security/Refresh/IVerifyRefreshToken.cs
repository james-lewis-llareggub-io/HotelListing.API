namespace HotelListing.API.v2_1.Contracts.Security.Refresh;

public interface IVerifyRefreshToken
{
    Task<PostLogin?> Verify(PostLogin dto);
}