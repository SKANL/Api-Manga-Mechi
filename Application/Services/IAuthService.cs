using MangaMechiApi.Application.DTOs;

namespace MangaMechiApi.Application.Services;

public interface IAuthService
{
    Task<AuthResultDto> LoginAsync(UserLoginDto loginDto);
    Task<AuthResultDto> RegisterAsync(UserRegisterDto registerDto);
}
