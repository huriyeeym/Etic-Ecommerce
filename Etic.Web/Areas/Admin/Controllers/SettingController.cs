using Microsoft.AspNetCore.Mvc;
using Etic.Business.Services;
using Etic.Entities;

namespace Etic.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SettingController : Controller
    {
        private readonly ISettingService _settingService;

        public SettingController(ISettingService settingService)
        {
            _settingService = settingService;
        }

        // Liste
        public IActionResult Index()
        {
            var settings = _settingService.GetAll();
            return View(settings);
        }

        // Düzenle - GET
        [HttpGet]
        public IActionResult Edit(string id)
        {
            var setting = _settingService.GetById(id);
            if (setting == null)
            {
                return NotFound();
            }

            return View(setting);
        }

        // Düzenle - POST
        [HttpPost]
        public IActionResult Edit(Setting setting)
        {
            if (ModelState.IsValid)
            {
                _settingService.Update(setting);
                TempData["Success"] = "Ayar başarıyla güncellendi!";
                return RedirectToAction("Index");
            }

            return View(setting);
        }
    }
}

