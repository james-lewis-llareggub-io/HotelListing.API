using HotelListing.API.Contracts.Security;
using HotelListing.API.Contracts.Security.Refresh;
using HotelListing.API.Middleware;
using HotelListing.API.Security;
using HotelListing.API.Security.Refresh;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.IdentityModel.Tokens;
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

builder.Services.AddControllers();
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
        Description = "Added url api versioning mechanism and deprecated WeatherForecast controller"
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
    options.DefaultApiVersion = new ApiVersion(2, 0);
    options.ReportApiVersions = true;
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
});

builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddAutoMapper(typeof(AutoMapperConfiguration));

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

builder.Host.UseSerilog((context, configuration) =>
{
    configuration
        .WriteTo.Console()
        .ReadFrom.Configuration(context.Configuration);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "V1");
        options.SwaggerEndpoint("/swagger/v2/swagger.json", "V2");
    });
}

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();