using Core.Entities;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    internal static class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        internal static IQueryable<TEntity> EvaluateQuery(IQueryable<TEntity> query, ISpecifications<TEntity> spec)
        {
            query = query.OrderBy(x => x.Id);
            if (spec.Includes.Any())
            {
                query = spec.Includes.Aggregate(query, (q, include) => q.Include(include));
            }
            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);
            }
            if (spec.OrderBy != null)
            {
                if (spec.IsOrderDescending)
                {
                    query = query.OrderByDescending(spec.OrderBy);
                }
                else
                {
                    query = query.OrderBy(spec.OrderBy);
                }
            }
            if (spec.IsPagingEnabled)
            {
                query = query.Skip(spec.Skip).Take(spec.Take);
            }
            return query;
        }
    }
}
