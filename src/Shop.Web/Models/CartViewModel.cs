using System.Collections.Generic;
using System.Linq;

namespace Shop.Web.Models
{
    public class CartViewModel
    {
        public IList<CartItemViewModel> Items { get; } = new List<CartItemViewModel>();
        public decimal TotalPrice => Items.Sum(i => i.TotalPrice);
    }
}
