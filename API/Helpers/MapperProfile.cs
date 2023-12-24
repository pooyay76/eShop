using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Entities.Identity;

namespace API.Helpers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(x => x.BrandName, o => o.MapFrom(s => s.Brand.Name))
                .ForMember(x => x.TypeName, o => o.MapFrom(s => s.Type.Name))
                .ForMember(x => x.PictureUrl, o => o.MapFrom<ProductValueResolver>());
            CreateMap<Address, AddressDto>();
        }
    }
}
