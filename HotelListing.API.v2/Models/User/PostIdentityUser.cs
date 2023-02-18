namespace HotelListing.API.v2.Models.User;

public class PostIdentityUser
{
    [EmailAddress] [Required] public string Email { get; set; }

    [PasswordPropertyText] [Required] public string Password { get; set; }
}