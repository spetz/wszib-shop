using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shop.Core.Services;
using Shop.Web.Framework;
using Shop.Web.Models;
using System;
using System.Collections.Generic;

namespace Shop.Web.Controllers
{
    [CookieAuth]
    public class OrdersController : BaseController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [HttpGet("orders")]
        public IActionResult Index()
        {
            var orders = _orderService.Browse(CurrentUserId);
            var viewModels = _mapper.Map<IEnumerable<OrderViewModel>>(orders);

            return View(viewModels);
        }

        [HttpPost("orders")]
        public IActionResult Create()
        {
            try
            {
                _orderService.Create(CurrentUserId);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}