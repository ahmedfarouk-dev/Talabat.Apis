using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entities;
using Talabat.Core.Interfaces;

namespace Talabat.Repositories
{
    public static class SpecificationEvaluator
    {


        public static IQueryable<T> GetQuery<T>(IQueryable<T> InputQuery, ISpecification<T> Spec) where T : ModelBase
        {

            var Query = InputQuery;
            if (Spec.Criteria != null)
                Query = Query.Where(Spec.Criteria);

            Query = Spec.Includes.Aggregate(Query, (Current, Includ) => Current.Include(Includ));


            return Query;
        }
    }
}
