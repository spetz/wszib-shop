using System.Collections.Generic;

namespace Shop.Web.Models
{
    public class CartViewModel
    {
        public IList<CartItemViewModel> Items { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
