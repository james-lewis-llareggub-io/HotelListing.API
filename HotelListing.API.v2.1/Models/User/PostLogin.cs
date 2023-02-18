namespace HotelListing.API.v2_1.Models.User;

public class PostLogin
{
    public string UserId { get; set; }

    public string Token { get; set; }

    public string RefreshToken { get; set; }
}