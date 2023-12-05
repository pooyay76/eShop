using Core.Entities;

namespace Core.Specifications
{
    public class ProductSpecifications : BaseSpecifications<Product>
    {
        public ProductSpecifications()
        {
        }

        public ProductSpecifications(int id) : base(x => x.Id == id)
        {
        }
    }
}
