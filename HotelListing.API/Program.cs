using HotelListing.API.Contracts.Security;
using HotelListing.API.Contracts.Security.Refresh;
using HotelListing.API.Middleware;
using HotelListing.API.Security;
using HotelListing.API.Security.Refresh;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Serilog;

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
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "AllowAll",
        policy => policy.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod()
    );
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
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();