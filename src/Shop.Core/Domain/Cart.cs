using System;
using System.Collections.Generic;
using System.Linq;

namespace Shop.Core.Domain
{
    public class Cart
    {
        private ISet<CartItem> _items = new HashSet<CartItem>();
        public IEnumerable<CartItem> Items => _items;
        public bool IsEmpty => !Items.Any();
        public decimal TotalPrice => Items.Sum(i => i.TotalPrice);

        public void AddProduct(Product product)
        {
            var item = _items.SingleOrDefault(x => x.ProductId == product.Id);
            if (item == null)
            {
                item = new CartItem(product);
                _items.Add(item);

                return;
            }
            item.IncreaseQuantity();
        }

        public void DeleteProduct(Guid productId)
        {
            var item = _items.SingleOrDefault(x => x.ProductId == productId);
            if (item == null)
            {
                throw new Exception($"Product with id: '{productId}' was not found in cart.");
            }
            if (item.Quantity == 1)
            {
                _items.Remove(item);

                return;
            }
            item.DecreaseQuantity();
        }

        public void Clear() => _items.Clear();
    }
}
