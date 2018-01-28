using Microsoft.AspNetCore.Mvc;
using Shop.Core.DTO;
using Shop.Core.Services;
using System;

namespace Shop.Service.Controllers
{
    [Route("[controller]")]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var products = _productService.GetAll();

            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var product = _productService.Get(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        public IActionResult Post([FromBody]ProductDto product)
        {
            var productId = Guid.NewGuid();
            _productService.Add(productId, product.Name, product.Category, product.Price);

            return Created($"products/{productId}", null);
        }
    }
}