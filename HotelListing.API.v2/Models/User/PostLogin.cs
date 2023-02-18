namespace HotelListing.API.v2.Models.User;

public class PostLogin
{
    public string UserId { get; set; }

    public string Token { get; set; }

    public string RefreshToken { get; set; }
}