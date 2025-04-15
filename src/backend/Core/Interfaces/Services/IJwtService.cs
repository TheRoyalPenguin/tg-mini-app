using Core.Models;
using Core.Utils;

namespace Core.Interfaces.Services;

public interface IJwtService
{
    Result<string> GenerateJwtToken(User user);
}
