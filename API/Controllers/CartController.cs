using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CartController : BaseApiController
    {
        private readonly ICartRepository _repository;

        public CartController(ICartRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public async Task<IActionResult> GetCartById(string id)
        {
            return Ok(await _repository.GetCartAsync(id) ?? new CustomerCart(id));
        }
        [HttpPost]
        public async Task<IActionResult> UpdateCart(CustomerCart cart)
        {
            return Ok(await _repository.UpdateCartAsync(cart));
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteCart(string id)
        {
            return Ok(await _repository.DeleteCartAsync(id));
        }
    }
}
