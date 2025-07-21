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

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5140);
});

const string LocalHostCorsPolicy = "AllowLocalHost";

builder.Services.AddCors(options =>
{
    options.AddPolicy(LocalHostCorsPolicy, builder =>
    {
        builder.WithOrigins("http://localhost:6969")
            .AllowAnyHeader()
            .AllowAnyMethod();
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

// setup supabase
var url = Environment.GetEnvironmentVariable("SUPABASE_URL") ?? throw new Exception("SUPABASE_URL was null");
var key = Environment.GetEnvironmentVariable("SUPABASE_KEY") ?? throw new Exception("SUPABASE_KEY was null");

var options = new SupabaseOptions
{
    AutoRefreshToken = true,
    AutoConnectRealtime = true,
};

builder.Services.AddSingleton(provider => new Client(url, key, options));

// build app
var app = builder.Build();

app.UseCors(LocalHostCorsPolicy);

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseRateLimiter();

app.MapControllers();

app.Run();