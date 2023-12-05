using Core.Entities;
using Core.Specifications;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        public Task<IReadOnlyList<T>> ListAllAsync();
        public Task<int> CountAsync(ISpecifications<T> spec);
        public Task<IReadOnlyList<T>> ListBySpecAsync(ISpecifications<T> spec);
        public Task<T> GetByIdAsync(int id);
        public Task<T> FindBySpecAsync(ISpecifications<T> spec);
    }
}
