using Core.Utils;
using Module = Core.Models.Module;

namespace Core.Interfaces.Repositories;

public interface IModuleRepository : IRepository<int, Module>
{
    
    Task<Result<ICollection<Module>>> GetAllByCourseIdAsync(int courseId);
}