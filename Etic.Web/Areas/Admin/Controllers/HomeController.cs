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
        private readonly IOrderService _orderService;

        public HomeController(
            ICategoryService categoryService,
            IProductService productService,
            IUserService userService,
            ISliderService sliderService,
            IOrderService orderService)
        {
            _categoryService = categoryService;
            _productService = productService;
            _userService = userService;
            _sliderService = sliderService;
            _orderService = orderService;
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
            
            // Sipariş Metrikleri (Gerçek Data - OrderService'ten)
            ViewBag.TotalOrders = _orderService.GetTotalCount();
            ViewBag.MonthlyOrders = _orderService.GetThisMonthOrders().Count;
            ViewBag.NewOrders = _orderService.GetCountByStatus(Etic.Entities.Enums.OrderStatus.Pending);
            ViewBag.DeliveredOrders = _orderService.GetCountByStatus(Etic.Entities.Enums.OrderStatus.Delivered);
            ViewBag.CancelledOrders = _orderService.GetCountByStatus(Etic.Entities.Enums.OrderStatus.Cancelled);
            
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

