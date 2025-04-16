using System.Security.Cryptography;
using System.Text;

namespace Core.Utils;

public static class TelegramAuthHelper
{
    public static bool ValidateTelegramData(long TgId, string Name, string Surname, string Patronymic,
        string PhoneNumber, string Hash, string botToken)
    {
        var dataCheckArr = new List<string>
        {
            "tg_id=" + TgId,
            "name=" + Name,
            "surname=" + Surname,
            "patronymic=" + Patronymic,
            "phone_number=" + PhoneNumber
        };

        var dataCheckString = string.Join("\n", dataCheckArr.OrderBy(s => s));
        var secretKey = Encoding.UTF8.GetBytes(botToken);

        using var hmac = new HMACSHA256(secretKey);
        var hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(dataCheckString));

        var computedHash = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

        return computedHash == Hash.ToLower();
    }
}
