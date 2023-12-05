using API.Dtos;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IGenericRepository<ProductBrand> _productBrandRepository;
        private readonly IGenericRepository<ProductType> _productTypeRepository;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> productRepository, IGenericRepository<ProductBrand> productBrandRepository, IGenericRepository<ProductType> productTypeRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _productBrandRepository = productBrandRepository;
            _productTypeRepository = productTypeRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<ProductDto>>> GetProducts([FromQuery] ProductSpecParams @params)
        {
            var spec = new ProductWithTypeAndBrandSpecifications(@params);
            var countSpec = new ProductWithFiltersForCountSpecification(@params);
            var products = await _productRepository.ListBySpecAsync(spec);
            var count = await _productRepository.CountAsync(countSpec);
            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductDto>>(products);
            return Ok(new Pagination<ProductDto>(@params.PageIndex, @params.PageSize, count, data));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var spec = new ProductSpecifications(id);
            return _mapper.Map<ProductDto>(await _productRepository.FindBySpecAsync(spec));
        }
        [HttpGet("brands")]
        public async Task<ActionResult<ProductBrand>> GetBrands()
        {
            return Ok(await _productBrandRepository.ListAllAsync());
        }
        [HttpGet("types")]
        public async Task<ActionResult<ProductType>> GetTypes()
        {
            return Ok(await _productTypeRepository.ListAllAsync());
        }
    }
}
