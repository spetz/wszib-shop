using System.Collections.Generic;

namespace Shop.Core.DTO
{
    public class CartDto
    {
        public IEnumerable<CartItemDto> Items { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
