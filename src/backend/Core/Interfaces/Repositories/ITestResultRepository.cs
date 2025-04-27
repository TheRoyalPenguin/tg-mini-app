using Core.Models;

namespace Core.Interfaces.Repositories;

public interface ITestResultRepository : IRepository<int, TestResult>
{
    Task<ICollection<TestResult>> GetAllByUser();
    Task<ICollection<TestResult>> GetAllByTest(int testId);
}