using Etic.Business.ControllerHandler;
using Microsoft.AspNetCore.Mvc;

namespace Etic.LiteWeb.Controllers
{
    public class AuthencationController : Controller
    {
        private readonly IAuthenticationControllerHandler _authencationControllerHandler;
        public IActionResult Index()
        {
          var r =  _authencationControllerHandler.UserLogin("asdasd", "asdasd", Response);
            return View();
        }
    }
}
