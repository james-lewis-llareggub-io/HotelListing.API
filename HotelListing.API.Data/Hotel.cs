﻿namespace HotelListing.API.Data;

public class Hotel : IHaveAnId
{
    public string Name { get; set; }

    public string Address { get; set; }

    public double Rating { get; set; }

    [ForeignKey(nameof(CountryId))] public int CountryId { get; set; }

    public Country Country { get; set; }
    public int Id { get; set; }
}