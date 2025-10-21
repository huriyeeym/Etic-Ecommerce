using Etic.Business.ControllerHandler;
using Etic.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace Etic.Web.Controllers
{
    public class ProductController : Controller
    {
        private IProductControllerHandler _productControllerHandler;

        public ProductController(IProductControllerHandler productControllerHandler)
        {
            _productControllerHandler = productControllerHandler;
        }

        public IActionResult Index(string name)
        {
            var result = _productControllerHandler.Get(name);
            return View(result);
        }
    }
}
