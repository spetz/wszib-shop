using Shop.Core.DTO;
using System;
using System.Collections.Generic;

namespace Shop.Core.Services
{
    public interface IOrderService
    {
        void Create(Guid userId);
        OrderDto Get(Guid id);
        IEnumerable<OrderDto> Browse(Guid userId);
    }
}
