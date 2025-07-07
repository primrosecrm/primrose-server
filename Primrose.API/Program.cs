using Primrose.API.Middleware;
using Primrose.API.Repositories;
using Primrose.API.Services.Authentication;
using Primrose.API.Entities.Login;

using Supabase;
using FluentValidation;
using Primrose.API.Validators.Services;
using Primrose.API.Services.Validators.Authentication;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5140);
});

builder.Services.AddControllers();

// validator service
builder.Services.AddScoped<IValidatorService, FluentValidatorService>();

// validators
builder.Services.AddScoped<IValidator<LoginRequest>, LoginRequestValidator>();
builder.Services.AddScoped<IValidator<RegisterRequest>, RegisterRequestValidator>();

// register repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();

// register services
builder.Services.AddScoped<IHashService, BCryptHashService>();
builder.Services.AddScoped<AuthenticationService, AuthenticationService>();


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

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();