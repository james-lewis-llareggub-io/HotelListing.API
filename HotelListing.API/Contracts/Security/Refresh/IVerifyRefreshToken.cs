namespace HotelListing.API.Contracts.Security.Refresh;

public interface IVerifyRefreshToken
{
    Task<PostLogin?> Verify(PostLogin dto);
}