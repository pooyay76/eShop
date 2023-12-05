using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly ShopContext _shopContext;

        public GenericRepository(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _shopContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _shopContext.Set<T>().FindAsync(id);
        }

        public async Task<T> FindBySpecAsync(ISpecifications<T> spec)
        {
            return await ApplySpecifications(spec).FirstOrDefaultAsync();
        }
        public async Task<IReadOnlyList<T>> ListBySpecAsync(ISpecifications<T> spec)
        {
            return await ApplySpecifications(spec).ToListAsync();
        }
        public async Task<int> CountAsync(ISpecifications<T> spec)
        {
            return await ApplySpecifications(spec).CountAsync();
        }
        private IQueryable<T> ApplySpecifications(ISpecifications<T> specifications)
        {
            return SpecificationEvaluator<T>.EvaluateQuery(_shopContext.Set<T>().AsQueryable(), specifications);
        }


    }
}
