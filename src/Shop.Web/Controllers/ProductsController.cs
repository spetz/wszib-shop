using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Core.DTO;
using Shop.Core.Services;
using Shop.Web.Framework;
using Shop.Web.Models;
using System;
using System.Linq;

namespace Shop.Web.Controllers
{
    [Route("products")]
    [CookieAuth("require-admin")]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        {
            var products = _productService
                .GetAll()
                .Select(p => new ProductViewModel(p));

            return View(products);
        }

        [HttpGet("add")]
        public IActionResult Add()
        {
            var viewModel = new AddOrUpdateProductViewModel();

            return View(viewModel);
        }

        [HttpPost("add")]
        public IActionResult Add(AddOrUpdateProductViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            _productService.Add(viewModel.Name, viewModel.Category, viewModel.Price);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("{id}/update")]
        public IActionResult Update(Guid id)
        {
            var product = _productService.Get(id);
            if (product == null)
            {
                return NotFound();
            }
            var viewModel = new AddOrUpdateProductViewModel(product);

            return View(viewModel);
        }

        [HttpPost("{id}/update")]
        public IActionResult Update(AddOrUpdateProductViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            _productService.Update(new ProductDto
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                Category = viewModel.Category,
                Price = viewModel.Price
            });

            return RedirectToAction(nameof(Index));
        }
    }
}