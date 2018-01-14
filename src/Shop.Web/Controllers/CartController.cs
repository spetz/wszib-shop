using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Shop.Core.Services;
using Shop.Web.Models;
using System;
using System.Linq;

namespace Shop.Web.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [Route("cart")]
    public class CartController : Controller
    {
        private readonly IMemoryCache _cache;
        private readonly IProductService _productService;

        public CartController(IMemoryCache cache, IProductService productService)
        {
            _cache = cache;
            _productService = productService;
        }

        public IActionResult Index()
        {
            var viewModel = _cache.Get<CartViewModel>($"{User.Identity.Name}:cart");

            return View(viewModel);
        }

        [HttpPost("items/{productId}/add")]
        public IActionResult Add(Guid productId)
        {
            var product = _productService.Get(productId);
            if (product == null)
            {
                return BadRequest();
            }
            var cart = _cache.Get<CartViewModel>($"{User.Identity.Name}:cart");
            var cartItem = cart.Items.SingleOrDefault(i => i.ProductId == productId);
            if (cartItem == null)
            {
                cartItem = new CartItemViewModel
                {
                    ProductId = productId,
                    ProductName = product.Name,
                    UnitPrice = product.Price,
                    Quantity = 1
                };
                cart.Items.Add(cartItem);
            }
            else
            {
                cartItem.Quantity++;
            }
            _cache.Set($"{User.Identity.Name}:cart", cart);

            //return Ok();
            return RedirectToAction("Index", "Products");
        }
    }
}