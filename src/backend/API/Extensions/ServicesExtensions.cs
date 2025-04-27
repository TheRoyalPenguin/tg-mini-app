using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Extensions;

public static class ServicesExtensions
{
    public static void AddPostgresDb(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(
            options => options.UseNpgsql(configuration.GetConnectionString(nameof(AppDbContext))));
    }
    
    public static void AddRedis(this IServiceCollection services, IConfiguration configuration)
    {
        var redisSettings = configuration.GetSection("RedisSettings").Get<RedisSettings>();

        if (redisSettings == null || string.IsNullOrEmpty(redisSettings.Host))
            throw new ArgumentException("Redis settings are missing or invalid.");

        var redisConfiguration = $"{redisSettings.Host}:{redisSettings.Port}";

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = redisConfiguration;
            options.InstanceName = "MdProcessor_"; 
        });
    }
}