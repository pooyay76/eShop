using System.Linq.Expressions;

namespace Core.Specifications
{
    public interface ISpecifications<T>
    {
        public Expression<Func<T, bool>> Criteria { get; }
        public Expression<Func<T, object>> OrderBy { get; }
        public List<Expression<Func<T, object>>> Includes { get; }
        public bool IsOrderDescending { get; }
        public bool IsPagingEnabled { get; }
        public int Take { get; }
        public int Skip { get; }

    }
}
