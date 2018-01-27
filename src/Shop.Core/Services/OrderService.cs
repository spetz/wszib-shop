using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Shop.Core.Domain;
using Shop.Core.DTO;
using Shop.Core.Extensions;
using Shop.Core.Repositories;

namespace Shop.Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ICartManager _cartManager;

        public OrderService(IOrderRepository orderRepository,
            IUserRepository userRepository,
            IMapper mapper, ICartManager cartManager)
        {
            _orderRepository = orderRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _cartManager = cartManager;
        }

        public void Create(Guid userId)
        {
            var user = _userRepository.Get(userId)
                .FailIfNull($"User with id: '{userId}' was not found.");
            var cart = _cartManager.Get(userId)
                .FailIfNull($"Cart was not found for user with id: '{userId}'.");
            var order = new Order(user, cart);
            _orderRepository.Add(order);
            cart.Clear();
            _cartManager.Set(userId, cart);
        }

        public OrderDto Get(Guid id)
        {
            var order = _orderRepository.Get(id);

            return order == null ? null : _mapper.Map<OrderDto>(order);
        }

        public IEnumerable<OrderDto> Browse(Guid userId)
            => _orderRepository.Browse(userId)
                               .Select(x => _mapper.Map<OrderDto>(x));
    }
}
