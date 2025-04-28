using Core.Models;
using Core.Utils;

namespace Core.Interfaces.Repositories;

public interface IResourceRepository : IRepository<int, Resource>
{
    Task<Result<ICollection<Resource>>> GetAllByModuleIdAsync(int moduleId);
    Task<Result> DeleteAsync(int id);
}