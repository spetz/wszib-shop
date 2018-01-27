using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Shop.Core.DTO;
using Shop.Core.Repositories;

namespace Shop.Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository,
            IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public void Create(Guid userId)
        {
            throw new NotImplementedException();
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
