using Core.Interfaces.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Services;

public class ModuleActivityBackgroundService(
    IServiceScopeFactory serviceScopeFactory,
    ILogger<ModuleActivityBackgroundService> logger)
    : BackgroundService
{
    private readonly TimeSpan _checkInterval = TimeSpan.FromDays(1);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Module Activity Background Service is starting.");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await Task.Delay(_checkInterval, stoppingToken);
            }
            catch (OperationCanceledException)
            {
                break;
            }
            
            try
            {
                using var scope = serviceScopeFactory.CreateScope();
                var moduleActivityService = scope.ServiceProvider.GetRequiredService<IModuleActivityService>();
                
                logger.LogInformation("Running module activity check...");
                var result = await moduleActivityService.CheckAndNotifyInactiveUsersAsync();
                
                if (!result.IsSuccess)
                {
                    logger.LogError("Module activity check failed: {Error}", result.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred while checking module activity");
            }
        }

        logger.LogInformation("Module Activity Background Service is stopping.");
    }
} 