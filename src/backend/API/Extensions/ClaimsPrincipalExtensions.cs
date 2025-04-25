using System.Security.Claims;
using Core.Models;

namespace Core.Utils;

public static class ClaimsPrincipalExtensions
{
    public static bool TryGetUserId(this ClaimsPrincipal? user, out int userId)
    {
        userId = default;

        if (user is null)
            return false;

        var claim = user.FindFirst(ClaimTypes.NameIdentifier);
        if (claim is null)
            return false;

        return int.TryParse(claim.Value, out userId);
    }
}
