namespace HotelListing.API.Models.User;

public class GetIdentityUser
{
    [Required] public string UserName { get; set; }

    [EmailAddress] [Required] public string Email { get; set; }
}