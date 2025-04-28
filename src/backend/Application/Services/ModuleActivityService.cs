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

            var userModuleAccesses = moduleAccesses
                .Where(ma => !ma.IsModuleCompleted && ma.IsModuleAvailable)
                .GroupBy(ma => ma.UserId);

            foreach (var userGroup in userModuleAccesses)
            {
                var userId = userGroup.Key;
                
                var userResult = await uow.Users.GetByIdAsync(userId);
                if (!userResult.IsSuccess)
                {
                    logger.LogError("Failure while getting user with id {UserId}: {ErrorMessage}", userId, userResult.ErrorMessage);
                    continue;
                }
                    
                if (userResult.Data.IsBanned)
                    continue;

                var user = userResult.Data;
                var userNotificationDaysLimit = user.NotificationDaysLimit switch
                {
                    0 => 7,
                    null => 7,
                    _ => (int)user.NotificationDaysLimit
                };
                
                var userNotificationTimestamp = DateTime.UtcNow.AddDays(-1 * userNotificationDaysLimit);
                foreach (var moduleAccess in userGroup)
                {
                    var isInactive = moduleAccess.LastActivity switch
                    {
                        null => true,
                        var lastActivity => lastActivity <= userNotificationTimestamp
                    };

                    if (!isInactive)
                        continue;

                    var notificationResult =
                        await notificationService.NotifyUserAboutModuleAsync(user.TgId, moduleAccess.ModuleId);
                    
                    if (notificationResult.IsSuccess)
                    {
                        logger.LogInformation(
                            "User {UserId} has not been active in module {ModuleId} for {DaysLimit} days. " +
                            "Notification sent",
                            userId,
                            moduleAccess.ModuleId,
                            userNotificationDaysLimit);
                    }
                    else
                    {
                        logger.LogError(
                            "User {UserId} has not been active in module {ModuleId} for for {DaysLimit} days. " +
                            "Notification failed",
                            userId,
                            moduleAccess.ModuleId,
                            userNotificationDaysLimit);
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