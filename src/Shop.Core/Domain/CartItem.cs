using System;

namespace Shop.Core.Domain
{
    public class CartItem
    {
        public Guid ProductId { get; private set; }
        public string ProductName { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal TotalPrice => Quantity * UnitPrice;

        public CartItem(Product product)
        {
            ProductId = product.Id;
            ProductName = product.Name;
            Quantity = 1;
            UnitPrice = product.Price;
        }
        
        public void IncreaseQuantity()
        {
            Quantity++;
        }

        public void DecreaseQuantity()
        {
            if (Quantity == 1)
            {
                throw new Exception("Quantity can not be lower than 1.");
            }
            Quantity--;
        }
    }
}
