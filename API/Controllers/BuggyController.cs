using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BuggyController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productRepository;

        public BuggyController(IGenericRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet("badrequest")]
        public IActionResult GetBadRequest()
        {
            return BadRequest();
        }
        [HttpGet("badrequest/{id}")]
        public IActionResult GetBadRequest(int id)
        {
            return Ok();
        }
        [HttpGet("notfound")]
        public IActionResult GetNotFound()
        {
            return NotFound();
        }
        [HttpGet("servererror")]
        public IActionResult GetServerError()
        {
            int.Parse("ssss");
            return Ok();
        }
    }
}
