# Inspiration

based on an udemy course available at https://www.udemy.com/course/ultimate-aspnet-5-web-api-development-guide with a few tweaks along the way

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

# API versions

## one console application per solution

keep the project structure simple containing all of the code 

use directories instead of projects

### too big?

if the project gets too big, consider splitting the service into more micro-services

## legacy versions

clone the main console application into a class library per version

- add the class library as a reference to the single console application containing the current version 

### [ApiVersion("x.0")] annotations

apply controller level annotations to each versioned class library

- do not apply controller level annotations to the main console application