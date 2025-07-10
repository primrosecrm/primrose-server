using Primrose.API.Middleware;
using Primrose.API.Repositories;
using Primrose.API.Services.Authentication;
using Primrose.API.Entities.Login;

using Supabase;
using FluentValidation;
using Primrose.API.Validators.Services;
using Primrose.API.Services.Validators.Authentication;
using Primrose.API.Validators;
using Primrose.API.Services.Authentication.Hashing;
using Primrose.API.Services.Authentication.Password;
using Primrose.API.Entities.RegisterUser;

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

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidationFilter>();
});

// validator service
builder.Services.AddScoped<IValidatorService, FluentValidatorService>();

// validators
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

var app = builder.Build();

app.UseCors(LocalHostCorsPolicy);

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();