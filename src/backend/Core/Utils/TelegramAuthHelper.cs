using System.Security.Cryptography;
using System.Text;

namespace Core.Utils;

public static class TelegramAuthHelper
{
    public static bool ValidateTelegramData(string initData, string botToken, int maxAgeSeconds = 86400)
    {
        // 1) Парсим и декодируем initData
        var parsed = initData
            .Split('&')
            .Select(p => p.Split(new[] { '=' }, 2))
            .Where(kv => kv.Length == 2)
            .ToDictionary(
                kv => kv[0],
                kv => Uri.UnescapeDataString(kv[1])
            );

        if (!parsed.TryGetValue("hash", out var receivedHash))
            return false;

        // 2) Собираем data_check_string из декодированных значений
        var dataCheckString = string.Join("\n",
            parsed
                .Where(kv => kv.Key != "hash")
                .OrderBy(kv => kv.Key, StringComparer.Ordinal)
                .Select(kv => $"{kv.Key}={kv.Value}")
        );

        // 3) secret_key = HMAC_SHA256(key="WebAppData", message=botToken)
        byte[] secretKey;
        using (var hmac1 = new HMACSHA256(Encoding.UTF8.GetBytes("WebAppData")))
        {
            secretKey = hmac1.ComputeHash(Encoding.UTF8.GetBytes(botToken));
        }

        // 4) computedHash = hex(HMAC_SHA256(key=secretKey, message=data_check_string))
        byte[] signatureBytes;
        using (var hmac2 = new HMACSHA256(secretKey))
        {
            signatureBytes = hmac2.ComputeHash(Encoding.UTF8.GetBytes(dataCheckString));
        }
        var computedHash = BitConverter.ToString(signatureBytes).Replace("-", "").ToLowerInvariant();

        if (!CryptographicOperations.FixedTimeEquals(
                Encoding.UTF8.GetBytes(computedHash),
                Encoding.UTF8.GetBytes(receivedHash.ToLowerInvariant())))
            return false;

        // 5) Проверяем staleness auth_date
        if (!parsed.TryGetValue("auth_date", out var authDateStr) ||
            !long.TryParse(authDateStr, out var ts))
            return false;

        var authTime = DateTimeOffset.FromUnixTimeSeconds(ts);
        if ((DateTimeOffset.UtcNow - authTime).TotalSeconds > maxAgeSeconds)
            return false;

        return true;
    }
}
