using Microsoft.AspNetCore.Mvc;
using Etic.Business.Services;
using Etic.Entities;

namespace Etic.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly ISliderService _sliderService;
        private readonly IWebHostEnvironment _environment;

        public SliderController(ISliderService sliderService, IWebHostEnvironment environment)
        {
            _sliderService = sliderService;
            _environment = environment;
        }

        // Liste
        public IActionResult Index()
        {
            var sliders = _sliderService.GetAll().Where(s => !s.IsDeleted).ToList();
            return View(sliders);
        }

        // Yeni Ekle - GET
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Yeni Ekle - POST
        [HttpPost]
        public async Task<IActionResult> Create(Slider slider, IFormFile? Image)
        {
            if (ModelState.IsValid)
            {
                // Resim yükleme
                if (Image != null && Image.Length > 0)
                {
                    slider.ImageUrl = await UploadImageAsync(Image);
                }

                slider.CreatedDate = DateTime.Now;
                slider.IsDeleted = false;
                _sliderService.Add(slider);

                TempData["Success"] = "Slider başarıyla eklendi!";
                return RedirectToAction("Index");
            }

            return View(slider);
        }

        // Düzenle - GET
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var slider = _sliderService.GetById(id);
            if (slider == null)
            {
                return NotFound();
            }

            return View(slider);
        }

        // Düzenle - POST
        [HttpPost]
        public async Task<IActionResult> Edit(Slider slider, IFormFile? Image)
        {
            if (ModelState.IsValid)
            {
                // Yeni resim yüklendi mi?
                if (Image != null && Image.Length > 0)
                {
                    // Eski resmi sil
                    if (!string.IsNullOrEmpty(slider.ImageUrl))
                    {
                        DeleteImage(slider.ImageUrl);
                    }

                    slider.ImageUrl = await UploadImageAsync(Image);
                }

                slider.UpdatedDate = DateTime.Now;
                _sliderService.Update(slider);

                TempData["Success"] = "Slider başarıyla güncellendi!";
                return RedirectToAction("Index");
            }

            return View(slider);
        }

        // Sil
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var slider = _sliderService.GetById(id);
            if (slider != null)
            {
                // Fiziksel dosyayı sil
                if (!string.IsNullOrEmpty(slider.ImageUrl))
                {
                    DeleteImage(slider.ImageUrl);
                }

                _sliderService.Delete(id);
                TempData["Success"] = "Slider başarıyla silindi!";
            }

            return RedirectToAction("Index");
        }

        // Resim Yükleme Helper
        private async Task<string> UploadImageAsync(IFormFile image)
        {
            var uploadPath = Path.Combine(_environment.WebRootPath, "uploads", "sliders");

            // Klasör yoksa oluştur
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            // Benzersiz dosya adı oluştur
            var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(image.FileName)}";
            var filePath = Path.Combine(uploadPath, fileName);

            // Dosyayı kaydet
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            return $"/uploads/sliders/{fileName}";
        }

        // Resim Silme Helper
        private void DeleteImage(string imageUrl)
        {
            var imagePath = Path.Combine(_environment.WebRootPath, imageUrl.TrimStart('/'));
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
        }
    }
}

