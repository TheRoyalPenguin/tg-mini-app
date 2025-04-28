using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models;
using Core.Utils;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class ModuleActivityService(
    IUnitOfWork uow, 
    ILogger<ModuleActivityService> logger,
    ITelegramNotificationService notificationService) : IModuleActivityService
{
    public async Task<Result> CheckAndNotifyInactiveUsersAsync()
    {
        try
        {
            var moduleAccessesResult = await uow.ModuleAccesses.GetAllAsync();
            if (!moduleAccessesResult.IsSuccess)
            {
                return Result.Failure(moduleAccessesResult.ErrorMessage!);
            }

            var moduleAccesses = moduleAccessesResult.Data;
            var oneWeekAgo = DateTime.UtcNow.AddDays(-7);

            var userModuleAccesses = moduleAccesses
                .Where(ma => !ma.IsModuleCompleted && ma.IsModuleAvailable)
                .GroupBy(ma => ma.UserId);

            foreach (var userGroup in userModuleAccesses)
            {
                var userId = userGroup.Key;
                
                var userResult = await uow.Users.GetByIdAsync(userId);
                if (!userResult.IsSuccess || userResult.Data.IsBanned)
                {
                    continue;
                }

                var user = userResult.Data;
                foreach (var moduleAccess in userGroup)
                {
                    var isInactive = moduleAccess.LastActivity switch
                    {
                        null => true,
                        var lastActivity => lastActivity <= oneWeekAgo
                    };

                    if (!isInactive)
                        continue;

                    var notificationResult =
                        await notificationService.NotifyUserAboutModuleAsync(user.TgId, moduleAccess.ModuleId);
                    
                    if (notificationResult.IsSuccess)
                    {
                        logger.LogInformation(
                            "User {UserId} has not been active in module {ModuleId} for more than a week. " +
                            "Notification sent",
                            userId,
                            moduleAccess.ModuleId);
                    }
                    else
                    {
                        logger.LogError(
                            "User {UserId} has not been active in module {ModuleId} for more than a week. " +
                            "Notification failed",
                            userId,
                            moduleAccess.ModuleId);
                    }
                }
            }

            return Result.Success();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error checking module activity");
            return Result.Failure($"Error checking module activity: {ex.Message}");
        }
    }
}