using Core.Interfaces;
using Core.Interfaces.Services;
using Core.Models;

namespace Application.Services;

public class EnrollmentService : IEnrollmentService
{
    private readonly IEnrollmentRepository repository;
    public EnrollmentService(IEnrollmentRepository repository)
    {
        this.repository = repository;
    }

    public async Task<ICollection<string>> GetCourseTitlesByUserId(int id)
    {
        var courses = await repository.GetCourseTitlesByUserId(id);
        var courseTitles = courses.Select(e => e.Title).ToList();
        return courseTitles;
    }
}