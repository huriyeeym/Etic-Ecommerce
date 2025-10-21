using Microsoft.AspNetCore.Mvc;

namespace Etic.LiteWeb.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
