using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CartController : BaseApiController
    {
        private readonly ICartRepository _repository;
        private readonly IMapper _mapper;

        public CartController(ICartRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetCartById(string id)
        {
            return Ok(_mapper.Map<CustomerCartDto>(await _repository.GetCartAsync(id)) ?? new CustomerCartDto { Id = id });
        }
        [HttpPost]
        public async Task<IActionResult> UpdateCart(CustomerCartDto cart)
        {
            return Ok(await _repository.UpdateCartAsync(new CustomerCart(cart.Id)
            {
                Items = MapCartItems(cart.Items),
                Id = cart.Id
            }));
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteCart(string id)
        {
            return Ok(await _repository.DeleteCartAsync(id));
        }
        private List<CartItem> MapCartItems(List<CartItemDto> cartItems)
        {
            return cartItems.Select(x => new CartItem()
            {
                Brand = x.Brand,
                Id = x.Id,
                PictureUrl = x.PictureUrl,
                Price = x.Price,
                ProductName = x.ProductName,
                Quantity = x.Quantity,
                Type = x.Type
            }).ToList();
        }
    }
}
