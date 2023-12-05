using System.Linq.Expressions;

namespace Core.Specifications
{
    public class BaseSpecifications<T> : ISpecifications<T>
    {
        public BaseSpecifications()
        {
        }

        public BaseSpecifications(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }
        public Expression<Func<T, bool>> Criteria { get; }
        public List<Expression<Func<T, object>>> Includes { get; } = new();

        public Expression<Func<T, object>> OrderBy { get; private set; }
        public bool IsOrderDescending { get; private set; }

        public bool IsPagingEnabled { get; private set; }

        public int Take { get; private set; }

        public int Skip { get; private set; }

        protected void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }
        protected void AddOrder(Expression<Func<T, object>> expression, bool isOrderDescending = false)
        {
            OrderBy = expression;
            IsOrderDescending = isOrderDescending;
        }
        protected void ApplyPaging(int take, int skip)
        {
            Take = take;
            Skip = skip;
            IsPagingEnabled = true;
        }
    }
}
