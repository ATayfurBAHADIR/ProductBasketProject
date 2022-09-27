using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.Text.Json.Serialization;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;


        }
        [HttpGet]
        public async Task<ActionResult<ICollection<Product>>> GetAll()
        {
            var products = await _productRepository.GetAll();
            return Ok(products);
        }

        [HttpGet("Id")]
        public async Task<ActionResult<Product>> GetById(Guid Id)
        {
            var products = await _productRepository.GetById(Id);
            return Ok(products);
        }
        [HttpPost]
        public async Task<ActionResult<Product>> Add([FromBody] Product value)
        {
            await _productRepository.Add(value);
            return Ok(value);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> Update([FromBody] Product value)
        {
            await _productRepository.Update(value);
            return Ok(_productRepository);
        }
        [HttpPost("Id")]
        public async Task<ActionResult<Product>> Remove(Guid Id)
        {
            await _productRepository.Remove(Id);
            return Ok();
        }
    }
}
