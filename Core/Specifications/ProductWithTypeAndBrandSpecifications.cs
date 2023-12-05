using Core.Entities;

namespace Core.Specifications
{
    public class ProductWithTypeAndBrandSpecifications : BaseSpecifications<Product>
    {

        public ProductWithTypeAndBrandSpecifications(ProductSpecParams @params)
        : base(x => (
        (string.IsNullOrEmpty(@params.Search) || x.Name.ToLower().Contains(@params.Search))
        &&
        (!@params.BrandId.HasValue || x.ProductBrandId == @params.BrandId)
         &&
         (!@params.TypeId.HasValue || x.ProductTypeId == @params.TypeId))
        )
        {

            AddInclude(x => x.Type);
            AddInclude(x => x.Brand);
            AddOrder(x => x.Name);

            ApplyPaging(@params.PageSize, @params.PageSize * (@params.PageIndex - 1));

            if (!string.IsNullOrEmpty(@params.Sort))
            {
                switch (@params.Sort)
                {
                    case "priceAsc":
                        AddOrder(x => x.Price, false);
                        break;
                    case "priceDesc":
                        AddOrder(x => x.Price, true);
                        break;
                    case "nameAsc":
                        AddOrder(x => x.Name, false);
                        break;
                    case "nameDesc":
                        AddOrder(x => x.Name, true);
                        break;
                    case "dateDesc":
                        AddOrder(x => x.Id, true);
                        break;
                    case "dateAsc":
                        AddOrder(x => x.Id, false);
                        break;
                    default:
                        AddOrder(x => x.Id, true);
                        break;
                }
            }
        }
        public ProductWithTypeAndBrandSpecifications(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.Type);
            AddInclude(x => x.Brand);
        }

    }
}
