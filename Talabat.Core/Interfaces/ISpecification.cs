using System.Linq.Expressions;
using Talabat.Core.Entities;

namespace Talabat.Core.Interfaces
{
    public interface ISpecification<T> where T : ModelBase
    {
        public Expression<Func<T, bool>> Criteria { get; set; }

        public List<Expression<Func<T, object>>> Includes { get; set; }
    }
}
