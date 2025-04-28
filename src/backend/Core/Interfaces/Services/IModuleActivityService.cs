using Core.Models;
using Core.Utils;

namespace Core.Interfaces.Services;

public interface IModuleActivityService
{
    Task<Result> CheckAndNotifyInactiveUsersAsync();
} 