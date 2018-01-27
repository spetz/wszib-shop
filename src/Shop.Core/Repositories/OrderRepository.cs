using System;
using System.Collections.Generic;
using System.Linq;
using Shop.Core.Domain;

namespace Shop.Core.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private static readonly ISet<Order> _orders = new HashSet<Order>();

        public void Add(Order order)
            => _orders.Add(order);

        public Order Get(Guid id)
            => _orders.SingleOrDefault(x => x.Id == id);

        public IEnumerable<Order> Browse(Guid userId)
            => _orders.Where(x => x.UserId == userId);
    }
}
