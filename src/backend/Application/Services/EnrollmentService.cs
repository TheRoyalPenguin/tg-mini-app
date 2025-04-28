using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models;
using Core.Utils;

namespace Application.Services;

public class EnrollmentService(IUnitOfWork uow) : IEnrollmentService
{
    public async Task<Result<ICollection<Course>>> GetCoursesByUserId(int id)
    {
        var coursesResult = await uow.Enrollments.GetCoursessByUserId(id);

        return coursesResult.IsSuccess
            ? Result<ICollection<Course>>.Success(coursesResult.Data)
            : Result<ICollection<Course>>.Failure(coursesResult.ErrorMessage!)!;
    }
    
    public async Task<Result<ICollection<User>>> GetUsersByCourseId(int id)
    {
        var userResult = await uow.Enrollments.GetUsersByCourseId(id);

        return userResult.IsSuccess 
            ? Result<ICollection<User>>.Success(userResult.Data)
            : Result<ICollection<User>>.Failure(userResult.ErrorMessage!)!;
    }
    
    public async Task<Result<Enrollment>> AddAsync(Enrollment enrollment)
    {
        var result = await uow.Enrollments.AddAsync(enrollment);
        if (!result.IsSuccess)
        {
            return Result<Enrollment>.Failure(result.ErrorMessage!)!;
        }

        await uow.SaveChangesAsync();
        
        return Result<Enrollment>.Success(result.Data);
    }

    public async Task<Result<Enrollment>> UpdateAsync(Enrollment enrollment)
    {
        var result = await uow.Enrollments.UpdateAsync(enrollment);
        if (!result.IsSuccess)
        {
            return Result<Enrollment>.Failure(result.ErrorMessage!)!;
        }

        await uow.SaveChangesAsync();
        
        return Result<Enrollment>.Success(result.Data);
    }

    public async Task<Result> DeleteAsync(int id)
    {
        var result = await uow.Enrollments.DeleteAsync(id);
        if (!result.IsSuccess)
        {
            return Result.Failure(result.ErrorMessage!);
        }
        
        await uow.SaveChangesAsync();

        return Result.Success();
    }

    public async Task<Result<Enrollment?>> GetByIdAsync(int id)
    {
        var result = await uow.Enrollments.GetByIdAsync(id);
        return result.IsSuccess 
            ? Result<Enrollment?>.Success(result.Data)
            : Result<Enrollment?>.Failure(result.ErrorMessage!);
    }

    public async Task<Result<ICollection<Enrollment>>> GetAllAsync()
    {
        var result = await uow.Enrollments.GetAllAsync();
        return result.IsSuccess 
            ? Result<ICollection<Enrollment>>.Success(result.Data) 
            : Result<ICollection<Enrollment>>.Failure(result.ErrorMessage!)!;
    }
}