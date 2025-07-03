using Primrose.API.Repositories;
using Primrose.API.Services.Authentication;
using Supabase;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5140);
});


builder.Services.AddControllers();

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

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.MapControllers();

app.Run();