using AutoMapper;
using Core.Interfaces;
using Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace Persistence.Repositories;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private IDbContextTransaction _currentTransaction;

    public UnitOfWork(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public bool HasActiveTransaction => _currentTransaction != null;

    public async Task StartTransactionAsync()
    {
        if (_currentTransaction != null)
            throw new InvalidOperationException("Transaction already in progress");

        _currentTransaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        if (_currentTransaction == null)
            throw new InvalidOperationException("No active transaction to commit");

        try
        {
            await _context.SaveChangesAsync();
            await _currentTransaction.CommitAsync();
        }
        finally
        {
            DisposeTransaction();
        }
    }

    public async Task RollbackTransactionAsync()
    {
        if (_currentTransaction == null)
            throw new InvalidOperationException("No active transaction to rollback");

        try
        {
            await _currentTransaction.RollbackAsync();
        }
        finally
        {
            DisposeTransaction();
        }
    }

    private void DisposeTransaction()
    {
        _currentTransaction?.Dispose();
        _currentTransaction = null;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        if (HasActiveTransaction)
            throw new InvalidOperationException("Use CommitTransactionAsync for transactional operations");

        return await _context.SaveChangesAsync(cancellationToken);
    }
    
    private ICourseRepository _courses;
    public ICourseRepository Courses => 
        _courses ??= new CourseRepository(_context, _mapper);

    private IEnrollmentRepository _enrollments;
    public IEnrollmentRepository Enrollments => 
        _enrollments ??= new EnrollmentRepository(_context, _mapper);

    private IModuleAccessRepository _moduleAccesses;
    public IModuleAccessRepository ModuleAccesses => 
        _moduleAccesses ??= new ModuleAccessRepository(_context, _mapper);

    private IModuleRepository _modules;
    public IModuleRepository Modules => 
        _modules ??= new ModuleRepository(_context, _mapper);

    private IRoleRepository _roles;
    public IRoleRepository Roles => 
        _roles ??= new RoleRepository(_context, _mapper);

    private ITelegramUserRepository _telegramUsers;
    public ITelegramUserRepository TelegramUsers => 
        _telegramUsers ??= new TelegramUserRepository(_context, _mapper);

    private IUserRepository _users;
    public IUserRepository Users =>
        _users ??= new UserRepository(_context, _mapper);

    public void Dispose()
    {
        DisposeTransaction();
        _context.Dispose();
    }
}