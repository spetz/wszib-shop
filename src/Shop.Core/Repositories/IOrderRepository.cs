using Shop.Core.Domain;
using System;
using System.Collections.Generic;

namespace Shop.Core.Repositories
{
    public interface IOrderRepository
    {
        void Add(Order order);
        Order Get(Guid id);
        IEnumerable<Order> Browse(Guid userId);
    }
}
