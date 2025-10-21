using Etic.Business.ControllerHandler;
using Etic.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Etic.Web.Controllers
{
    public class CheckOutController : Controller
    {
        ICheckOutControllerHandler _checkOutControllerHandler;

        public CheckOutController(ICheckOutControllerHandler checkOutControllerHandler)
        {
            _checkOutControllerHandler = checkOutControllerHandler;
        }

        public IActionResult Index()
        {
            var basket = _checkOutControllerHandler.GetBasketProducts(Request);
            if (basket == null || basket.Count == 0)
            {
                return RedirectToRoute("default");
            }
            var user = _checkOutControllerHandler.GetUser(Request);
            CheckOutModel model = new CheckOutModel();
            model.BasketProducts = basket;
            model.User = user ?? null;
            
            return View(model);
        }
    }
}
