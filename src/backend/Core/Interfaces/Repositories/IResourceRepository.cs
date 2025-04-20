using Core.Models;
using Core.Utils;

namespace Core.Interfaces.Repositories;

public interface IResourceRepository : IRepository<int, Resource>
{
    Task<IEnumerable<TestingQuestion>> GetAllByModuleIdAsync(int moduleId);
    Task<Result> DeleteAsync(int id);
}