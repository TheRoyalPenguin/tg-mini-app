using Core.Models;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Core.Utils;
using Core.Interfaces.Services;

namespace Application.Services;

public class JwtService : IJwtService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<JwtService> _logger;

    public JwtService(IConfiguration configuration, ILogger<JwtService> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public Result<string> GenerateJwtToken(User user)
    {
        try
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim("tg_id", user.TgId.ToString()),
                new Claim(ClaimTypes.Name, user.Name ?? ""),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var keyString = _configuration["AppSettings:KeyString"];

            if (string.IsNullOrEmpty(keyString))
            {
                throw new Exception("JWT ключ не задан в конфигурации");
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                //issuer: _configuration["Jwt:Issuer"],
                //audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: creds);

            return Result<string>.Success(new JwtSecurityTokenHandler().WriteToken(token));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при генерации JWT для пользователя с Id {UserId}", user.Id);
            return Result<string>.Failure("Ошибка при генерации JWT для пользователя с Id " + user.Id)!;
        }
    }
}
