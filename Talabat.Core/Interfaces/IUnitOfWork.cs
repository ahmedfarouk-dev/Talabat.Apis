using Talabat.Core.Entities;

namespace Talabat.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {


        IRepositories<T> Repository<T>() where T : ModelBase;

        Task<int> Complete();

    }
}
