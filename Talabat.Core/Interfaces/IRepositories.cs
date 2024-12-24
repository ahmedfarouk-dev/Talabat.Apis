using Talabat.Core.Entities;

namespace Talabat.Core.Interfaces
{
    public interface IRepositories<T> where T : ModelBase
    {

        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();


    }
}
