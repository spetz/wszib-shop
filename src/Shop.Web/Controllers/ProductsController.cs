using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Core.DTO;
using Shop.Core.Services;
using Shop.Web.Framework;
using Shop.Web.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Web.Controllers
{
    [Route("products")]
    [CookieAuth("require-admin")]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly IServiceClient _serviceClient;

        public ProductsController(IProductService productService,
            IServiceClient serviceClient)
        {
            _productService = productService;
            _serviceClient = serviceClient;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            //var products = _productService
            //    .GetAll()
            //    .Select(p => new ProductViewModel(p));
            var products = await _serviceClient.GetProductsAsync();
            var viewModels = products.Select(p => new ProductViewModel(p));

            return View(viewModels);
        }

        [HttpGet("add")]
        public IActionResult Add()
        {
            var viewModel = new AddOrUpdateProductViewModel();

            return View(viewModel);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(AddOrUpdateProductViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            await _serviceClient.AddProductAsync(viewModel.Name, viewModel.Category, viewModel.Price);

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