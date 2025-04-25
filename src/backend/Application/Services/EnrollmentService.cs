using Core.Interfaces;
using Core.Interfaces.Services;
using Core.Models;
using Core.Utils;

namespace Application.Services;

public class EnrollmentService : IEnrollmentService
{
    private readonly IEnrollmentRepository repository;
    public EnrollmentService(IEnrollmentRepository repository)
    {
        this.repository = repository;
    }

    public async Task<Result<ICollection<Course>>> GetCoursesByUserId(int id)
    {
        var coursesResult = await repository.GetCoursessByUserId(id);

        if (!coursesResult.IsSuccess)
        {
            return Result<ICollection<Course>>.Failure(coursesResult.ErrorMessage);
        }

        return Result<ICollection<Course>>.Success(coursesResult.Data);
    }
    
    public async Task<Result<ICollection<User>>> GetUsersByCourseId(int id)
    {
        var userResult = await repository.GetUsersByCourseId(id);

        return !userResult.IsSuccess 
            ? Result<ICollection<User>>.Failure(userResult.ErrorMessage!)! 
            : Result<ICollection<User>>.Success(userResult.Data);
    }
    
    public async Task<Result<Enrollment>> AddAsync(Enrollment enrollment)
    {
        var result = await repository.AddAsync(enrollment);
        if (!result.IsSuccess)
        {
            return Result<Enrollment>.Failure(result.ErrorMessage);
        }

        return Result<Enrollment>.Success(result.Data);
    }

    public async Task<Result<Enrollment>> UpdateAsync(Enrollment enrollment)
    {
        var result = await repository.UpdateAsync(enrollment);
        if (!result.IsSuccess)
        {
            return Result<Enrollment>.Failure(result.ErrorMessage);
        }

        return Result<Enrollment>.Success(result.Data);
    }

    public async Task<Result> DeleteAsync(int id)
    {
        var result = await repository.DeleteAsync(id);
        if (!result.IsSuccess)
        {
            return Result.Failure(result.ErrorMessage);
        }

        return Result.Success();
    }

    public async Task<Result<Enrollment?>> GetByIdAsync(int id)
    {
        var result = await repository.GetByIdAsync(id);
        if (!result.IsSuccess)
        {
            return Result<Enrollment?>.Failure(result.ErrorMessage);
        }

        return Result<Enrollment?>.Success(result.Data);
    }

    public async Task<Result<ICollection<Enrollment>>> GetAllAsync()
    {
        var result = await repository.GetAllAsync();
        if (!result.IsSuccess)
        {
            return Result<ICollection<Enrollment>>.Failure(result.ErrorMessage);
        }

        return Result<ICollection<Enrollment>>.Success(result.Data);
    }
}