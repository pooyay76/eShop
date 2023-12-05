using Core.Entities;

namespace Core.Interfaces
{
    public interface IProductRepository
    {
        public Task<Product> GetProductAsync(int id);
        public Task<IReadOnlyList<Product>> GetAllProductsAsync();
    }
}
