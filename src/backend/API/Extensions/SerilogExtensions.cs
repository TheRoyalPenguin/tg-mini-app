using Serilog;
using Serilog.Events;

namespace API.Extensions;

public static class SerilogExtensions
{
    public static IHostBuilder UseSeqLogging(this IHostBuilder hostBuilder)
    {
        return hostBuilder.UseSerilog((ctx, lc) =>
        {
            lc
              .ReadFrom.Configuration(ctx.Configuration)
              .Enrich.FromLogContext()
              .Enrich.WithEnvironmentUserName();
        });
    }
}
