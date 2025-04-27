using AutoMapper;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models;
using Core.Utils;

namespace Application.Services;

public class AdminService(IUnitOfWork uow, IMapper mapper) : IAdminService
{
    public async Task<Result<ICollection<User>>> GetUsersInCourse(int courseId)
    {
        var repositoryResult = await uow.Users.GetAllByCourseIdAsync(courseId);
        
        return repositoryResult.IsSuccess
            ? Result<ICollection<User>>.Success(repositoryResult.Data)
            : Result<ICollection<User>>.Failure(repositoryResult.ErrorMessage!)!;
    }
    
    public async Task<Result<User>> GetConcreteUserInCourse(int userId, int courseId)
    {
        var repositoryResult = await uow.Users.GetOneInCourseAsync(userId, courseId);
        
        return repositoryResult.IsSuccess
            ? Result<User>.Success(repositoryResult.Data)
            : Result<User>.Failure(repositoryResult.ErrorMessage!)!;
    }

    public async Task<Result<User>> GetConcreteUser(int userId)
    {
        var repositoryResult = await uow.Users.GetOneWithAllCourses(userId);
        
        return repositoryResult.IsSuccess
            ? Result<User>.Success(repositoryResult.Data)
            : Result<User>.Failure(repositoryResult.ErrorMessage!)!;
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

    public async Task<Result<ICollection<TestResult>>> GetTestResultsByCourse(int courseId)
    {
        var repositoryResult = await uow.TestResults.GetAllByCourse(courseId);
        
        return repositoryResult.IsSuccess
            ? Result<ICollection<TestResult>>.Success(repositoryResult.Data)
            : Result<ICollection<TestResult>>.Failure(repositoryResult.ErrorMessage!)!;
    }

    public async Task<Result<ICollection<TestResult>>> GetTestResultsByUser(int userId)
    {
        var repositoryResult = await uow.TestResults.GetAllByUser(userId);
        
        return repositoryResult.IsSuccess
            ? Result<ICollection<TestResult>>.Success(repositoryResult.Data)
            : Result<ICollection<TestResult>>.Failure(repositoryResult.ErrorMessage!)!;
    }

    public async Task<Result<ICollection<TestResult>>> GetTestResultsForUserByCourse(int userId, int courseId)
    {
        var repositoryResult = await uow.TestResults.GetAllForUserByCourse(userId, courseId);
        
        return repositoryResult.IsSuccess
            ? Result<ICollection<TestResult>>.Success(repositoryResult.Data)
            : Result<ICollection<TestResult>>.Failure(repositoryResult.ErrorMessage!)!;
    }
}