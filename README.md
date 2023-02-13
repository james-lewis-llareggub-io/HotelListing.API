# Entity Framework Migrations

`dotnet-ef migrations add InitialMigration`

`dotnet-ef migrations add SeededCountriesAndHotels`

`dotnet-ef database update`

# Security 

- use data transfer objects to prevent over posting attacks as per scaffolded suggestion https://go.microsoft.com/fwlink/?linkid=2123754
        
    
            data transfer objects live in `HotelListing.API.Models`

                each transfer object has a naming convention prefix indicating the task
                    - Post (create)
                    - Put (update)
                    - Get
                    - GetDetail (sometimes required to return things that you do not want returned as part of a list)


# SOLID

## Single Responsibility

- add validation annotations (e.g. [Required]) to data transfer objects and not entity framework POCO