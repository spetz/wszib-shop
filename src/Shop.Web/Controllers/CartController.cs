using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shop.Core.Services;
using Shop.Web.Framework;
using Shop.Web.Models;
using System;

namespace Shop.Web.Controllers
{
    [Route("cart")]
    [CookieAuth]
    public class CartController : BaseController
    {
        private readonly ICartService _cartService;
        private readonly IMapper _mapper;

        public CartController(ICartService cartService, IMapper mapper)
        {
            _cartService = cartService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var cart = _cartService.Get(CurrentUserId);
            if (cart == null)
            {
                _cartService.Create(CurrentUserId);
                cart = _cartService.Get(CurrentUserId);
            }
            var viewModel = _mapper.Map<CartViewModel>(cart);

            return View(viewModel);
        }

        [HttpPost("items/{productId}")]
        public IActionResult Add(Guid productId)
        {
            _cartService.AddProduct(CurrentUserId, productId);

            return Ok();
        }

        [HttpDelete("items/{productId}")]
        public IActionResult Delete(Guid productId)
        {
            _cartService.DeleteProduct(CurrentUserId, productId);

            return Ok();
        }
    }
}