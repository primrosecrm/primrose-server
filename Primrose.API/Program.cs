using Primrose.API.Models.Authentication;
using Primrose.API.Services.Authentication;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.AddScoped<IAuthenticationService<UserCredential, UserAuthenticationResult>, AuthenticationService>();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.MapControllers();

app.Run();