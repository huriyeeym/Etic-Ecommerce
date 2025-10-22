using Microsoft.AspNetCore.Mvc;
using Etic.Business.Services;

namespace Etic.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly IUserService _userService;
        private readonly ISliderService _sliderService;

        public HomeController(
            ICategoryService categoryService,
            IProductService productService,
            IUserService userService,
            ISliderService sliderService)
        {
            _categoryService = categoryService;
            _productService = productService;
            _userService = userService;
            _sliderService = sliderService;
        }

        public IActionResult Index()
        {
            // Ürün İstatistikleri
            var allProducts = _productService.GetAll().Where(p => !p.IsDeleted).ToList();
            ViewBag.ProductCount = allProducts.Count;
            ViewBag.ActiveProductCount = _productService.GetAllActive().Count();
            ViewBag.OutOfStockCount = _productService.GetOutOfStockCount();
            
            // Kategori ve Slider
            ViewBag.CategoryCount = _categoryService.GetAllCategories().Where(c => !c.IsDeleted).Count();
            ViewBag.SliderCount = _sliderService.GetAll().Where(s => !s.IsDeleted).Count();
            
            // Sipariş Metrikleri (Şimdilik mock data - sipariş sistemi eklenince güncellenecek)
            ViewBag.TotalOrders = 0;
            ViewBag.MonthlyOrders = 0;
            ViewBag.NewOrders = 0;
            ViewBag.DeliveredOrders = 0;
            ViewBag.CancelledOrders = 0;
            
            // Son eklenen ürünler
            var latestProducts = allProducts
                .OrderByDescending(p => p.CreatedDate)
                .Take(5)
                .ToList();
            ViewBag.LatestProducts = latestProducts;

            // Düşük stoklu ürünler
            var lowStockProducts = allProducts
                .Where(p => p.Stock > 0 && p.Stock <= 10)
                .OrderBy(p => p.Stock)
                .Take(5)
                .ToList();
            ViewBag.LowStockProducts = lowStockProducts;

            return View();
        }
    }
}

