using Microsoft.AspNetCore.Mvc;
using Etic.Business.Services;
using Etic.Entities;

namespace Etic.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // Liste
        public IActionResult Index()
        {
            var categories = _categoryService.GetAllCategories();
            return View(categories);
        }

        // Yeni Ekle - GET
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Yeni Ekle - POST
        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                category.CreatedDate = DateTime.Now;
                category.IsDeleted = false;
                category.Sort = 0; // Default sıralama
                
                _categoryService.Add(category);
                
                TempData["Success"] = "Kategori başarıyla eklendi!";
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // Düzenle - GET
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var category = _categoryService.GetById(id);
            
            if (category == null)
            {
                return NotFound();
            }
            
            return View(category);
        }

        // Düzenle - POST
        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                category.UpdatedDate = DateTime.Now;
                
                _categoryService.Update(category);
                
                TempData["Success"] = "Kategori başarıyla güncellendi!";
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // Sil
        [HttpPost]
        public IActionResult Delete(int id)
        {
            _categoryService.Delete(id);
            TempData["Success"] = "Kategori başarıyla silindi!";
            return RedirectToAction("Index");
        }

        // Toplu Silme
        [HttpPost]
        public IActionResult BulkDelete([FromBody] int[] ids)
        {
            try
            {
                if (ids == null || ids.Length == 0)
                {
                    return Json(new { success = false, message = "Silinecek kategori seçilmedi" });
                }

                foreach (var id in ids)
                {
                    _categoryService.Delete(id);
                }

                return Json(new { success = true, message = $"{ids.Length} kategori başarıyla silindi" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Silme işlemi başarısız: " + ex.Message });
            }
        }

        // Slug Kontrol
        [HttpGet]
        public IActionResult CheckSlug(string seoLink, int? id)
        {
            var categories = _categoryService.GetAllCategories();
            var exists = categories.Any(c => c.SeoLink == seoLink && c.Id != (id ?? 0) && !c.IsDeleted);
            return Json(new { exists = exists });
        }
    }
}

