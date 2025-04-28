using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Utils;

namespace Application.Services;

public class UserService(IUnitOfWork uow) : IUserService
{
    public async Task<Result> SetNotificationDaysDelay(int userId, int daysDelay)
    {
        var getUserResult = await uow.Users.GetByIdAsync(userId);
        if (!getUserResult.IsSuccess)
            return Result.Failure(getUserResult.ErrorMessage!);

        var userModel = getUserResult.Data;
        userModel.NotificationDaysLimit = daysDelay;

        var editUserResult = await uow.Users.UpdateAsync(userModel);
        if (!editUserResult.IsSuccess)
            return Result.Failure(editUserResult.ErrorMessage!);

        await uow.SaveChangesAsync();

        return Result.Success();
    }
}