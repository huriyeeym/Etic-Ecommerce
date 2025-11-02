using Microsoft.AspNetCore.Mvc;
using Etic.Business.Services;

namespace Etic.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // Liste
        public IActionResult Index()
        {
            var users = _userService.GetAll().OrderByDescending(u => u.CreatedDate).ToList();
            return View(users);
        }

        // Detay
        public IActionResult Details(int id)
        {
            var user = _userService.GetById(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // Sil
        [HttpPost]
        public IActionResult Delete(int id)
        {
            _userService.Delete(id);
            TempData["Success"] = "Kullanıcı başarıyla silindi!";
            return RedirectToAction("Index");
        }
    }
}

