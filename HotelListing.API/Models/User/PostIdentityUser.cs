using System.ComponentModel;

namespace HotelListing.API.Models.User;

public class PostIdentityUser
{
    [EmailAddress] [Required] public string Email { get; set; }

    [PasswordPropertyText] [Required] public string Password { get; set; }
}