using Microsoft.AspNetCore.Mvc;
using Etic.Business.Services;
using Etic.Entities;

namespace Etic.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IProductCategoryService _productCategoryService;
        private readonly IProductImageService _productImageService;
        private readonly IWebHostEnvironment _environment;

        public ProductController(
            IProductService productService,
            ICategoryService categoryService,
            IProductCategoryService productCategoryService,
            IProductImageService productImageService,
            IWebHostEnvironment environment)
        {
            _productService = productService;
            _categoryService = categoryService;
            _productCategoryService = productCategoryService;
            _productImageService = productImageService;
            _environment = environment;
        }

        // Liste
        public IActionResult Index()
        {
            var products = _productService.GetAll().Where(p => !p.IsDeleted).ToList();
            return View(products);
        }

        // Yeni Ekle - GET
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = _categoryService.GetAllCategories();
            return View();
        }

        // Yeni Ekle - POST
        [HttpPost]
        public async Task<IActionResult> Create(Product product, List<int> CategoryIds, List<IFormFile> Images)
        {
            if (ModelState.IsValid)
            {
                // 1. Ürünü kaydet
                product.CreatedDate = DateTime.Now;
                product.IsDeleted = false;
                _productService.Add(product);

                // 2. Kategorileri ilişkilendir
                if (CategoryIds != null && CategoryIds.Any())
                {
                    _productCategoryService.UpdateProductCategories(product.Id, CategoryIds);
                }

                // 3. Resimleri yükle
                if (Images != null && Images.Any())
                {
                    await UploadImagesAsync(product.Id, Images);
                }

                TempData["Success"] = "Ürün başarıyla eklendi!";
                return RedirectToAction("Index");
            }

            ViewBag.Categories = _categoryService.GetAllCategories();
            return View(product);
        }

        // Düzenle - GET
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = _productService.GetById(id);
            if (product == null)
            {
                return NotFound();
            }

            ViewBag.Categories = _categoryService.GetAllCategories();
            ViewBag.SelectedCategories = _productCategoryService.GetCategoriesByProductId(id)
                .Select(pc => pc.CategoryId).ToList();
            ViewBag.ProductImages = _productImageService.GetAllByProductId(id);

            return View(product);
        }

        // Düzenle - POST
        [HttpPost]
        public async Task<IActionResult> Edit(Product product, List<int> CategoryIds, List<IFormFile> Images)
        {
            if (ModelState.IsValid)
            {
                product.UpdatedDate = DateTime.Now;
                _productService.Update(product);

                // Kategorileri güncelle
                if (CategoryIds != null)
                {
                    _productCategoryService.UpdateProductCategories(product.Id, CategoryIds);
                }

                // Yeni resimler eklendi mi?
                if (Images != null && Images.Any())
                {
                    await UploadImagesAsync(product.Id, Images);
                }

                TempData["Success"] = "Ürün başarıyla güncellendi!";
                return RedirectToAction("Index");
            }

            ViewBag.Categories = _categoryService.GetAllCategories();
            ViewBag.SelectedCategories = _productCategoryService.GetCategoriesByProductId(product.Id)
                .Select(pc => pc.CategoryId).ToList();
            ViewBag.ProductImages = _productImageService.GetAllByProductId(product.Id);

            return View(product);
        }

        // Sil
        [HttpPost]
        public IActionResult Delete(int id)
        {
            _productService.Delete(id);
            TempData["Success"] = "Ürün başarıyla silindi!";
            return RedirectToAction("Index");
        }

        // Resim Sil (AJAX)
        [HttpPost]
        public IActionResult DeleteImage(int id)
        {
            var image = _productImageService.GetById(id);
            if (image != null)
            {
                // Fiziksel dosyayı sil
                var imagePath = Path.Combine(_environment.WebRootPath, image.ImageUrl.TrimStart('/'));
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }

                _productImageService.Delete(id);
                return Json(new { success = true });
            }

            return Json(new { success = false });
        }

        // Resim Yükleme Helper
        private async Task UploadImagesAsync(int productId, List<IFormFile> images)
        {
            var uploadPath = Path.Combine(_environment.WebRootPath, "uploads", "products");
            
            // Klasör yoksa oluştur
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            int sort = _productImageService.GetAllByProductId(productId).Count;

            foreach (var image in images)
            {
                if (image.Length > 0)
                {
                    // Benzersiz dosya adı oluştur
                    var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(image.FileName)}";
                    var filePath = Path.Combine(uploadPath, fileName);

                    // Dosyayı kaydet
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }

                    // Veritabanına kaydet
                    var productImage = new ProductImage
                    {
                        ProductId = productId,
                        ImageUrl = $"/uploads/products/{fileName}",
                        Sort = sort++
                    };

                    _productImageService.Add(productImage);
                }
            }
        }
    }
}

