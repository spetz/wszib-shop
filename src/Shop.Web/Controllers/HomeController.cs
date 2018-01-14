using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Shop.Web.Models;

namespace Shop.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet("forbidden")]
        public IActionResult Forbidden()
            => View();
    }
}
