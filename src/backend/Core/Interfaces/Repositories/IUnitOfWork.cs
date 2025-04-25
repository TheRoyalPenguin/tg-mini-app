namespace Core.Interfaces.Repositories;

public interface IUnitOfWork : IDisposable
{
    ICourseRepository Courses { get; }
    IEnrollmentRepository Enrollments { get; }
    IModuleAccessRepository ModuleAccesses { get; }
    IModuleRepository Modules { get; }
    IRoleRepository Roles { get; }
    ITelegramUserRepository TelegramUsers { get; }
    IUserRepository Users { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    
    Task StartTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
    bool HasActiveTransaction { get; }
}