# v1

## deprecation

moved weather forecast api functionality to this independent class library to support backwards compatability

v1 will eventually be deprecated in main API via `Program.cs` by changing `ApiVersion` to `2.0`

    builder.Services.AddApiVersioning(options =>
    {
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.DefaultApiVersion = new ApiVersion(1, 0);
        options.ReportApiVersions = true;
        options.ApiVersionReader = ApiVersionReader.Combine(
            new QueryStringApiVersionReader("api-version"),
            new HeaderApiVersionReader("X-Version"),
            new MediaTypeApiVersionReader("ver")
        );
    });