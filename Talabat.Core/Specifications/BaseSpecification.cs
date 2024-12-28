using System.Linq.Expressions;
using Talabat.Core.Entities;
using Talabat.Core.Interfaces;

namespace Talabat.Core.Specifications
{
    public class BaseSpecification<T> : ISpecification<T> where T : ModelBase
    {
        public Expression<Func<T, bool>> Criteria { get; set; } = null!;
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
        public Expression<Func<T, object>> OrderBy { get; set; }
        public Expression<Func<T, object>> OrderByDesc { get; set; }
        public int Take { get; set; }
        public int Skip { get; set; }
        public bool IsPagination { get; set; }

        public BaseSpecification()
        {

        }

        public BaseSpecification(Expression<Func<T, bool>> ExpressionCriteria)
        {
            Criteria = ExpressionCriteria;
        }

        public void OrderByAsc(Expression<Func<T, object>> OrderBy)
        {
            this.OrderBy = OrderBy;

        }
        public void OrderByDsc(Expression<Func<T, object>> OrderByDesc)
        {
            this.OrderByDesc = OrderByDesc;
        }

        public void ApplyPagination(int skip, int take)
        {
            IsPagination = true;
            this.Skip = skip;
            this.Take = take;
        }
    }
}
