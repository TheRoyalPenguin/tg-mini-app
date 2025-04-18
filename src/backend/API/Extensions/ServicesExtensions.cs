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
}