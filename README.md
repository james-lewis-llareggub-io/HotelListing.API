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


# response caching
## enabled in Program.cs
### add response caching to web application builder's servce
    builder.Services.AddResponseCaching(options =>
    {
        options.MaximumBodySize = cacheSettings.MaximumSizeInMegabytes;
        options.UseCaseSensitivePaths = true;
    });

### add middleware delegate to web application to add header information to response
    app.UseResponseCaching();
    
    app.Use(async (context, next) =>
    {
        context.Response.GetTypedHeaders().CacheControl = new CacheControlHeaderValue()
        {
            Public = true,
            MaxAge = TimeSpan.FromSeconds(cacheSettings.MaximumAgeInSeconds)
        };
        context.Response.Headers[HeaderNames.Vary] = new []{ "Accept-Encoding" };
        await next();
    });


## configurable via appsettings.json
    "Cache": {
        "MaximumSizeInMegabytes": 1024,
        "MaximumAgeInSeconds": 60
    }

# OData (Open Data Protocol)

https://learn.microsoft.com/en-us/odata/overview

introduced in api version v2.2 

## select example

retrieve only the name property from a list of all hotels

https://localhost:7076/api/v2.2/Hotels?$select=name

    [
      {
        "Name": "Sandals Resort and Spa"
      },
      {
        "Name": "Comfort Suites"
      },

## query documentation

https://learn.microsoft.com/en-us/aspnet/web-api/overview/odata-support-in-aspnet-web-api/supporting-odata-query-options

https://learn.microsoft.com/en-us/odata/concepts/queryoptions-usage?source=recommendations#top--skip


# URL API versioning

make calls to url based api version explicit from day one

### v1

https://localhost:7076/api/v1/Login

### v2 

https://localhost:7076/api/v2/Login

## one console application per solution

keep the project structure simple containing all of the code 

use directories instead of projects

### too big?

if the project gets too big, consider splitting the service into more micro-services

## legacy versions

clone the main console application into a class library per version

- add the class library as a reference to the single console application containing the current version 

### [ApiVersion("x.0")] annotations

apply controller level api version annotations to each versioned class library

- do not apply controller level api version annotations to the main console application

 >> minimize api versioning and route duplication by annotating abstract classes

### update version process

DISCLAIMER: a work in progress...

- clone the main console application into a class library e.g. `HotelListing.API.v7.csproj`
- DO NOT DELETE ANYTHING except
  - obvious files e.g. `Program.cs`, `appsettings.json`, `appsettings.Development.json`
  - `Migrations` folder because it contains entity framework generated files
  - `logs` folder because it contains daily generated development seq files
  - `Configurations` folder because these should not contain destructive changes until legacy version are no longer supported
  - `Properties` folder because you do not need a launchSettings.json in a class library
  - `Data` folder because there can only be one source of truth for the database context (see migration strategy below)
- annotate abstract controllers with `[ApiVersion("x.0")]`
- upgrade `Program.cs` with appropriate `ApiVersion(x, y)`;
  
          builder.Services.AddApiVersioning(options =>
          {
              options.DefaultApiVersion = new ApiVersion(2, 0);
              options.ReportApiVersions = true;
              options.ApiVersionReader = new UrlSegmentApiVersionReader();
          });
  
- add entry to swagger ui 


            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "V1");
                options.SwaggerEndpoint("/swagger/v2/swagger.json", "V2");
            });

- update all namespaces in new class library accordingly e.g. from `HotelListing.API.v6` to `HotelListing.API.v7`
  - except things referencing the `Data` package containing the entity framework database context
    
## QUESTIONS
### multiple entity framework database contexts?
initial thoughts

if you need to run two or more api versions at the same time against the same database you need to:
- make non-destructive additive changes until the deprecated version is completely unsupported and deleted

#### SOLUTION
##### one single source of truth
- only 1 entity framework database context per web application
- only 1 `Data` folder per web application (aka solution)

##### migration strategy
- delete a legacy api version console library when no longer supported e.g. v1
- make destructive database changes after deleting a versioned project that previously prevented such changes
- create an entity framework migration script

# TODO
### add new database column required in new version not in old
### delete database column required in old version not in new
### change database column datatype e.g. guid to long
### support 2+ deprecated versions 
### remove support from 2+ deprecrated versions