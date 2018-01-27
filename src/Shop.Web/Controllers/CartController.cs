using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Shop.Core.Services;
using Shop.Web.Models;
using System;

namespace Shop.Web.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [Route("cart")]
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
            var viewModel = _mapper.Map<CartViewModel>(cart);

            return View(viewModel);
        }

        [HttpPost("items/{productId}/add")]
        public IActionResult Add(Guid productId)
        {
            _cartService.AddProduct(CurrentUserId, productId);

            //return Ok();
            return RedirectToAction("Index", "Products");
        }
    }
}