using System;

namespace Shop.Core.Domain
{
    public class OrderItem
    {
        public Guid ProductId { get; }
        public string ProductName { get; }
        public int Quantity { get; }
        public decimal UnitPrice { get; }
        public decimal TotalPrice { get; }

        public OrderItem(CartItem item)
        {
            ProductId = item.ProductId;
            ProductName = item.ProductName;
            Quantity = item.Quantity;
            UnitPrice = item.UnitPrice;
            TotalPrice = item.TotalPrice;
        }
    }
}
