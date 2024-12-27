using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entities;
using Talabat.Core.Interfaces;

namespace Talabat.Repositories
{
    public static class SpecificationEvaluator<T> where T : ModelBase
    {


        public static IQueryable<T> GetQuery(IQueryable<T> InputQuery, ISpecification<T> Spec)
        {

            var Query = InputQuery;
            if (Spec.Criteria != null)
                Query = Query.Where(Spec.Criteria);

            Query = Spec.Includes.Aggregate(Query, (Current, Includ) => Current.Include(Includ));


            return Query;
        }
    }
}
