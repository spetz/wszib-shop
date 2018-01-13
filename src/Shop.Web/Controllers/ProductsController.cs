using Microsoft.AspNetCore.Mvc;
using Shop.Core.Domain;
using System.Collections.Generic;

namespace Shop.Web.Controllers
{
    public class ProductsController : Controller
    {
        private static readonly List<Product> _products = new List<Product>
        {
            new Product("Laptop", "Electronics", 3000),
            new Product("Jeans", "Trousers", 150),
            new Product("Hammer", "Tools", 47)
        };

        public IActionResult Index()
        {
            return View(_products);
        }
    }
}