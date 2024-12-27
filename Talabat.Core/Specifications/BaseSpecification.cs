using System.Linq.Expressions;
using Talabat.Core.Entities;
using Talabat.Core.Interfaces;

namespace Talabat.Core.Specifications
{
    public class BaseSpecification<T> : ISpecification<T> where T : ModelBase
    {
        public Expression<Func<T, bool>> Criteria { get; set; } = null!;
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();

        public BaseSpecification()
        {

        }

        public BaseSpecification(Expression<Func<T, bool>> ExpressionCriteria)
        {
            Criteria = ExpressionCriteria;
        }
    }
}
