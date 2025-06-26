using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.EntityFrameworkCore;
using Primrose.API.Context;
using Primrose.API.Repositories;
using Primrose.API.Services.Authentication;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.AddScoped<AuthenticationService, AuthenticationService>();
builder.Services.AddScoped<UserRepository, UserRepository>();
builder.Services.AddScoped<IHashService, BCryptHashService>();

DotNetEnv.Env.Load();

var connStr = Environment.GetEnvironmentVariable("POSTGRES_CONNECTION");
builder.Services.AddDbContext<PrimroseContext>(
    options => options.UseNpgsql(connStr)
);

var app = builder.Build();

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.MapControllers();

app.Run();