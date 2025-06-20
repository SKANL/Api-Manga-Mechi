using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MangaMechiApi.Application.DTOs;
using MangaMechiApi.Core.Entities;
using MangaMechiApi.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;

namespace MangaMechiApi.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;
    private readonly IPasswordHasher<User> _passwordHasher;

    public AuthService(IUserRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
        _passwordHasher = new PasswordHasher<User>();
    }

    public async Task<AuthResultDto> RegisterAsync(UserRegisterDto registerDto)
    {
        var existingUser = await _userRepository.GetByUsernameAsync(registerDto.Username);
        if (existingUser != null)
        {
            throw new InvalidOperationException("El nombre de usuario ya está registrado.");
        }
        var user = new User { Username = registerDto.Username };
        user.PasswordHash = _passwordHasher.HashPassword(user, registerDto.Password);
        await _userRepository.CreateAsync(user);
        return new AuthResultDto { Username = user.Username, Token = GenerateJwtToken(user) };
    }

    public async Task<AuthResultDto> LoginAsync(UserLoginDto loginDto)
    {
        var user = await _userRepository.GetByUsernameAsync(loginDto.Username);
        if (user == null)
            throw new UnauthorizedAccessException("Invalid credentials");
        var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginDto.Password);
        if (result == PasswordVerificationResult.Failed)
            throw new UnauthorizedAccessException("Invalid credentials");
        return new AuthResultDto { Username = user.Username, Token = GenerateJwtToken(user) };
    }

    private string GenerateJwtToken(User user)
    {
        var jwtSettings = _configuration.GetSection("Jwt");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: creds
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

// NOTA IMPORTANTE:
// Este servicio NO "loguea" al usuario con el token, sino que solo lo genera y valida.
// El control de acceso a los endpoints protegidos lo realiza el middleware de autorización de ASP.NET Core,
// que verifica el token JWT en cada petición y permite o deniega el acceso automáticamente.
}
