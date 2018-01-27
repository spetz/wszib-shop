using Microsoft.AspNetCore.Mvc;
using Shop.Web.Framework;

namespace Shop.Web.Controllers
{
    [CookieAuth]
    public class OrdersController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}