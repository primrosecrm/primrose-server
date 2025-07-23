using Primrose.Middleware;
using Primrose.Repositories;
using Primrose.Services.Authentication;
using Primrose.Entities.LoginUser;
using Primrose.Validators.Services;
using Primrose.Services.Hashing;
using Primrose.Services.Password;
using Primrose.Entities.RegisterUser;
using Primrose.API.Services.Authentication;

using System.Threading.RateLimiting;
using FluentValidation;
using Supabase;
using Primrose.Validators.Filters;
using Primrose.Validators.Authentication;
using Microsoft.AspNetCore.Mvc;
using Primrose.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(7050, listenOptions =>
    {
        listenOptions.UseHttps();
    });
});

const string LocalHostCorsPolicy = "AllowLocalHost";

// for production
// const string ProductionCorsPolicy = "AllowTrustedOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(LocalHostCorsPolicy, builder =>
    {
        // replace with frontend domain eventually
        builder.WithOrigins("http://localhost:6969")
            .AllowAnyHeader()
            .AllowAnyMethod();

            // in production
            // .AllowCredentials();
    });
});

builder.Services.AddRateLimiter(options =>
{
    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
        RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown",
            factory: _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 100,
                Window = TimeSpan.FromMinutes(1)
            }));
});

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
});

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidationFilter>();
});

// register validator service
builder.Services.AddScoped<IValidatorService, FluentValidatorService>();

// register validators
builder.Services.AddScoped<IValidator<LoginUserRequest>, LoginRequestValidator>();
builder.Services.AddScoped<IValidator<RegisterUserRequest>, RegisterRequestValidator>();

// register repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();

// register services
builder.Services.AddScoped<IHashService, BCryptHashService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IPasswordService, PasswordService>();
builder.Services.AddScoped<IPasswordPolicy, OwaspPasswordPolicy>();

DotNetEnv.Env.Load();


// set up jwt
var jwtSecret = Environment.GetEnvironmentVariable("JWT_SECRET") 
    ?? throw new Exception("JWT_SECRET was null");

builder.Services.AddSingleton(new JwtProvider(jwtSecret));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret)),
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddHttpContextAccessor();

// set up supabase
var url = Environment.GetEnvironmentVariable("SUPABASE_URL") 
    ?? throw new Exception("SUPABASE_URL was null");

var key = Environment.GetEnvironmentVariable("SUPABASE_KEY") 
    ?? throw new Exception("SUPABASE_KEY was null");

var options = new SupabaseOptions
{
    AutoRefreshToken = true,
    AutoConnectRealtime = true,
};

builder.Services.AddSingleton(provider => new Client(url, key, options));

builder.Services.AddHttpsRedirection(options =>
{
    options.HttpsPort = 7050;
});

// build app
var app = builder.Build();

app.UseCors(LocalHostCorsPolicy);

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseRateLimiter();

app.MapControllers();

app.Run(); 