namespace HotelListing.API.Models.User;

public class PostIdentityUser : GetIdentityUser
{
    [Required] public string Password { get; set; }
}