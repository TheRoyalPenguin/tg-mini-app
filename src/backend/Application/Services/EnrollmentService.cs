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

    public async Task<Result<ICollection<string>>> GetCourseTitlesByUserId(int id)
    {
        var coursesResult = await repository.GetCoursessByUserId(id);

        if (!coursesResult.IsSuccess)
        {
            return Result<ICollection<string>>.Failure(coursesResult.ErrorMessage);
        }

        var courseTitles = coursesResult.Data.Select(c => c.Title).ToList();
        return Result<ICollection<string>>.Success(courseTitles);
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

    public async Task<Result> DeleteAsync(Enrollment enrollment)
    {
        var result = await repository.DeleteAsync(enrollment);
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