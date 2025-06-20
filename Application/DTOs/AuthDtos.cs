using MangaMechiApi.Core.Entities;

namespace MangaMechiApi.Application.DTOs;

public class UserLoginDto
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class UserRegisterDto
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class AuthResultDto
{
    public string Token { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
}
