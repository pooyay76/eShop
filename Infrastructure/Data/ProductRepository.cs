using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly ShopContext _shopContext;

        public ProductRepository(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public async Task<IReadOnlyList<Product>> GetAllProductsAsync()
        {
            return await _shopContext.Products.ToListAsync();

        }

        public async Task<Product> GetProductAsync(int id)
        {
            return await _shopContext.Products.FindAsync(id);
        }
    }
}
