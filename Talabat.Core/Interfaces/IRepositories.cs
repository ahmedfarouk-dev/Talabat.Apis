using System.Linq.Expressions;
using Talabat.Core.Entities;

namespace Talabat.Core.Interfaces
{
    public interface IRepositories<T> where T : ModelBase
    {

        public Task<T?> GetByIdAsync(int id);
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<T?> GetByIdWithSpecAsync(ISpecification<T> Spec);
        public Task<IEnumerable<T>> GetAllWithSpecAsync(ISpecification<T> Spec);

        public Task<int> GetCount(Expression<Func<T, bool>> CountExperssion);

        public Task<int> GetCountWithSpecAsync(ISpecification<T> Spec);
    }
}
