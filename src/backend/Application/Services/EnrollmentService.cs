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
}