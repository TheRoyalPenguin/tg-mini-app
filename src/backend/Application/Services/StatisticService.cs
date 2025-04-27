using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models;
using Core.Utils;

namespace Application.Services;

public class StatisticService(IUnitOfWork uow) : IStatisticService
{
    public async Task<Result<TestResult>> AddStatisticAsync(TestResult entity)
    {
        var repoResult = await uow.TestResults.AddAsync(entity);
        return repoResult.IsSuccess
            ? Result<TestResult>.Success(repoResult.Data)
            : Result<TestResult>.Failure(repoResult.ErrorMessage!)!;
    }
    
    public async Task<Result<ICollection<User>>> GetUsersInCourseStatisticAsync(int courseId)
    {
        var repositoryResult = await uow.Users.GetAllByCourseIdAsync(courseId);
        
        return repositoryResult.IsSuccess
            ? Result<ICollection<User>>.Success(repositoryResult.Data)
            : Result<ICollection<User>>.Failure(repositoryResult.ErrorMessage!)!;
    }
    
    public async Task<Result<User>> GetConcreteUserInCourseStatisticAsync(int userId, int courseId)
    {
        var repositoryResult = await uow.Users.GetOneInCourseAsync(userId, courseId);
        
        return repositoryResult.IsSuccess
            ? Result<User>.Success(repositoryResult.Data)
            : Result<User>.Failure(repositoryResult.ErrorMessage!)!;
    }

    public async Task<Result<User>> GetConcreteUserStatisticAsync(int userId)
    {
        var repositoryResult = await uow.Users.GetOneWithAllCourses(userId);
        
        return repositoryResult.IsSuccess
            ? Result<User>.Success(repositoryResult.Data)
            : Result<User>.Failure(repositoryResult.ErrorMessage!)!;
    }
}