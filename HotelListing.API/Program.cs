using HotelListing.API.Contracts.Security;
using HotelListing.API.Contracts.Security.Refresh;
using HotelListing.API.Data.Repositories;
using HotelListing.API.Middleware;
using HotelListing.API.Security;
using HotelListing.API.Security.Refresh;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.OData;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Serilog;

const string title = "HotelListing.API";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("HotelListingDbConnectionString");
builder.Services.AddDbContext<HotelListingDbContext>(options => { options.UseSqlServer(connectionString); });

builder.Services.AddIdentityCore<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>(CreateRefreshToken.LoginProvider)
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<HotelListingDbContext>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = title,
        Version = "v1",
        Description = "Initial draft (including WeatherForecast controller)"
    });
    options.SwaggerDoc("v2", new OpenApiInfo
    {
        Title = title,
        Version = "v2",
        Description = "Url api versioning mechanism (deprecated WeatherForecast controller)"
    });
    options.SwaggerDoc("v2.1", new OpenApiInfo
    {
        Title = title,
        Version = "v2.1",
        Description = "Response caching with configurable size and time app settings"
    });
    options.SwaggerDoc("v2.2", new OpenApiInfo
    {
        Title = title,
        Version = "v2.2",
        Description = "OData data access protocol for get all methods"
    });
    options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
    options.DocInclusionPredicate((docName, apiDesc) => apiDesc.GroupName == docName);
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "AllowAll",
        policy => policy.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod()
    );
});

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(2, 2);
    options.ReportApiVersions = true;
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
});

builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddAutoMapper(typeof(AutoMapperConfiguration));
builder.Services.AddAutoMapper(typeof(HotelListing.API.v2.Configurations.AutoMapperConfiguration));
builder.Services.AddAutoMapper(typeof(HotelListing.API.v2_1.Configurations.AutoMapperConfiguration));

builder.Services
    .AddScoped(typeof(IRepository<>), typeof(Repository<>))
    .AddScoped<ICountriesRepository, CountriesRepository>()
    .AddScoped<IHotelsRepository, HotelsRepository>()
    .AddScoped<IClaimsProvider, ClaimsProvider>()
    .AddScoped<ISigningCredentialsProvider, SigningCredentialsProvider>()
    .AddScoped<IJwtSecurityTokenProvider, JwtSecurityTokenProvider>()
    .AddScoped<IJwtSettingsConfiguration, JwtSettingsConfiguration>()
    .AddScoped<ICreateRefreshToken, CreateRefreshToken>()
    .AddScoped<IVerifyRefreshToken, VerifyRefreshToken>()
    .AddScoped<ICreatePostLogin, CreatePostLogin>();

var provider = new SigningCredentialsProvider(builder.Configuration);
var jwt = new JwtSettingsConfiguration(builder.Configuration);
var cacheSettings = new CacheSettingsConfiguration(builder.Configuration);

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
            ValidIssuer = jwt.Issuer,
            ValidAudience = jwt.Audience,
            IssuerSigningKey = provider.GetSymmetricSecurityKey()
        };
    });

builder.Services.AddResponseCaching(options =>
{
    options.MaximumBodySize = cacheSettings.MaximumSizeInMegabytes;
    options.UseCaseSensitivePaths = true;
});

builder.Host.UseSerilog((context, configuration) =>
{
    configuration
        .WriteTo.Console()
        .ReadFrom.Configuration(context.Configuration);
});

builder.Services
    .AddControllers()
    .AddOData(options =>
    {
        options
            .Select()
            .Filter()
            .OrderBy()
            .Count()
            .SkipToken()
            .SetMaxTop(10)
            .Expand();
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v2.2/swagger.json", "V2.2");
        options.SwaggerEndpoint("/swagger/v2.1/swagger.json", "V2.1");
        options.SwaggerEndpoint("/swagger/v2/swagger.json", "V2");
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "V1");
    });
}

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseResponseCaching();

app.Use(async (context, next) =>
{
    var version = context.GetRequestedApiVersion();
    if (version is { MajorVersion: >= 2, MinorVersion: >= 1 })
    {
        context.Response.GetTypedHeaders().CacheControl = new CacheControlHeaderValue
        {
            Public = true,
            MaxAge = TimeSpan.FromSeconds(cacheSettings.MaximumAgeInSeconds)
        };
        context.Response.Headers[HeaderNames.Vary] = new[] { "Accept-Encoding" };
    }

    await next();
});

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();