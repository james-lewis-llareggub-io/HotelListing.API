﻿namespace HotelListing.API.v2.Contracts.Security;

public interface ICreatePostLogin
{
    Task<PostLogin> Create(IdentityUser user);
}