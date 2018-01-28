using Microsoft.AspNetCore.Mvc;

namespace Shop.Service.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("")]
        public IActionResult Get()
            => Content("Welcome to the Shop Service.");
    }
}