using AutoMapper;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models;
using Core.Utils;

namespace Application.Services;

public class AdminService(IUnitOfWork uow, IMapper mapper) : IAdminService
{
    public Result<ICollection<User>> GetUsersByCourse(int courseId)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> RegisterUserOnCourse(int userId, int courseId)
    {
        await uow.StartTransactionAsync();

        var newEnrollment = new Enrollment
        {
            CourseId = courseId,
            UserId = userId,
            IsCourseCompleted = false,
            EnrollmentDate = DateOnly.FromDateTime(DateTime.Now)
        };
        
        var addEnrolmentResult = await uow.Enrollments.AddAsync(newEnrollment);
        if (!addEnrolmentResult.IsSuccess)
        {
            await uow.RollbackTransactionAsync();
            return Result.Failure(addEnrolmentResult.ErrorMessage!);
        }
        
        var addModuleAccessesResult = await uow.ModuleAccesses.AddAccessesForEveryModuleForUserAsync(userId, courseId);
        if (!addModuleAccessesResult.IsSuccess)
        {
            await uow.RollbackTransactionAsync();
            return Result.Failure(addModuleAccessesResult.ErrorMessage!);
        }
        
        await uow.CommitTransactionAsync();
        
        return Result.Success();
    }
}