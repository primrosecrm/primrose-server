using Microsoft.IdentityModel.Tokens;
using Primrose.API.Entities.Login;
using Primrose.API.Entities.Register;
using Primrose.API.Repositories;
using Primrose.API.Services.Authentication;
using Primrose.API.Services.Authentication.Hashing;
using Primrose.API.Services.Authentication.Pasword;
using Supabase;

namespace Primrose.Tests;

public class AuthenticationTests
{
    private readonly Client _supabaseClient;
    private readonly IUserRepository _userRepository;
    private readonly IHashService _hashService;
    private readonly IPasswordService _passwordService;
    private readonly AuthenticationService _authService;

    public AuthenticationTests()
    {
        var baseDir = AppDomain.CurrentDomain.BaseDirectory;
        var envPath = Path.Combine(baseDir, "..", "..", "..", ".env");
        envPath = Path.GetFullPath(envPath);

        Console.WriteLine($"Loading .env from: {envPath}");
        DotNetEnv.Env.Load(envPath);


        var url = Environment.GetEnvironmentVariable("SUPABASE_URL") ?? throw new Exception("SUPABASE_URL was null");
        var key = Environment.GetEnvironmentVariable("SUPABASE_KEY") ?? throw new Exception("SUPABASE_KEY was null");

        _supabaseClient = new Client(url, key);
        _supabaseClient.InitializeAsync().GetAwaiter().GetResult();

        _userRepository = new UserRepository(_supabaseClient);
        _hashService = new BCryptHashService();
        _passwordService = new PasswordService(new OwaspPasswordPolicy());

        _authService = new AuthenticationService(_userRepository, _hashService, _passwordService);
    }

    [Fact]
    public async Task LoginUser_WithValidCredentials_ReturnsAuthenticated()
    {
        var request = new LoginUserRequest
        {
            Email = "ashton3@gmail.com",
            Password = "Password1!"
        };

        var result = await _authService.LoginUser(request);
        Assert.True(result.IsAuthenticated);

        Assert.True(result.ErrorResult.Errors.IsNullOrEmpty());
        Assert.True(result.Success);
    }

    [Fact]
    public async Task LoginUser_WithInValidCredentials_ReturnsNotAuthenticated()
    {
        var request = new LoginUserRequest
        {
            Email = "somerandomemail@randomprovider.com",
            Password = "9nw4RGvmk2vMwVS9"
        };

        var result = await _authService.LoginUser(request);
        Assert.False(result.IsAuthenticated);

        Assert.True(result.ErrorResult.Errors.IsNullOrEmpty());
        Assert.True(result.Success);
    }

    [Fact]
    public async Task RegisterUser_WithValidCredentials_ReturnsCreated()
    {
        var guid = Guid.NewGuid();

        var request = new RegisterUserRequest
        {
            Email = $"{guid}@gmail.com",
            Password = "Password1!",
            Name = "newUser"
        };

        var result = await _authService.RegisterUser(request);
        Assert.True(result.CreatedSuccessfully);

        Assert.True(result.ErrorResult.Errors.IsNullOrEmpty());
        Assert.True(result.Success);
    }

    [Fact]
    public async Task RegisterUser_WithInValidCredentials_ReturnsNotCreated()
    {
        var request = new RegisterUserRequest
        {
            Email = $"an invalid email",
            Password = "Password1!",
            Name = "newUser"
        };

        var result = await _authService.RegisterUser(request);
        Assert.False(result.CreatedSuccessfully);

        Assert.False(result.ErrorResult.Errors.IsNullOrEmpty());
        Assert.False(result.Success);
    }
}