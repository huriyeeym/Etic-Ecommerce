using Microsoft.AspNetCore.Mvc;
using Etic.Business.Services;
using Etic.Entities;
using Etic.Entities.Enums;

namespace Etic.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// Sipariş Yönetimi Controller
    /// Admin panelde siparişleri görüntüleme, güncelleme, durum değiştirme
    /// </summary>
    [Area("Admin")]
    public class OrderController : Controller
    {
        // Servisler: Veritabanı işlemleri için
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;
        private readonly IProductService _productService;

        // Constructor: Dependency Injection ile servisleri alıyoruz
        public OrderController(
            IOrderService orderService,
            IUserService userService,
            IProductService productService)
        {
            _orderService = orderService;
            _userService = userService;
            _productService = productService;
        }

        /// <summary>
        /// Sipariş Listesi Sayfası
        /// URL: /Admin/Order
        /// </summary>
        public IActionResult Index()
        {
            // Tüm siparişleri getir
            var orders = _orderService.GetAll();

            // Dashboard metriklerini ViewBag'e ekle
            ViewBag.TotalOrders = _orderService.GetTotalCount();
            ViewBag.PendingOrders = _orderService.GetCountByStatus(OrderStatus.Pending);
            ViewBag.ConfirmedOrders = _orderService.GetCountByStatus(OrderStatus.Confirmed);
            ViewBag.ShippedOrders = _orderService.GetCountByStatus(OrderStatus.Shipped);
            ViewBag.DeliveredOrders = _orderService.GetCountByStatus(OrderStatus.Delivered);
            ViewBag.CancelledOrders = _orderService.GetCountByStatus(OrderStatus.Cancelled);
            ViewBag.ReturnedOrders = _orderService.GetCountByStatus(OrderStatus.Returned);
            ViewBag.TotalRevenue = _orderService.GetTotalRevenue();

            // Breadcrumb
            ViewData["Title"] = "Order Management";
            ViewData["Breadcrumb"] = @"
                <li class=""breadcrumb-item""><a href=""/Admin"">Dashboard</a></li>
                <li class=""breadcrumb-item active"">Orders</li>
            ";

            return View(orders);
        }

        /// <summary>
        /// Sipariş Detayları Sayfası
        /// URL: /Admin/Order/Details/5
        /// </summary>
        [HttpGet]
        public IActionResult Details(int id)
        {
            // Siparişi getir
            var order = _orderService.GetById(id);
            
            if (order == null)
            {
                TempData["Error"] = "Sipariş bulunamadı!";
                return RedirectToAction("Index");
            }

            // Siparişteki ürünleri getir
            var orderProducts = _orderService.GetOrderProducts(id);
            ViewBag.OrderProducts = orderProducts;

            // Breadcrumb
            ViewData["Title"] = $"Order #{order.Id}";
            ViewData["Breadcrumb"] = $@"
                <li class=""breadcrumb-item""><a href=""/Admin"">Dashboard</a></li>
                <li class=""breadcrumb-item""><a href=""/Admin/Order"">Orders</a></li>
                <li class=""breadcrumb-item active"">Order #{order.Id}</li>
            ";

            return View(order);
        }

        /// <summary>
        /// Sipariş Durumu Değiştir (AJAX)
        /// POST: /Admin/Order/UpdateStatus
        /// </summary>
        [HttpPost]
        public IActionResult UpdateStatus(int orderId, int status)
        {
            try
            {
                var order = _orderService.GetById(orderId);
                
                if (order == null)
                {
                    return Json(new { success = false, message = "Sipariş bulunamadı!" });
                }

                // Enum'a çevir
                var newStatus = (OrderStatus)status;

                // Durumu güncelle
                _orderService.UpdateStatus(orderId, newStatus, "Admin");

                // Durum isimlerini Türkçe'ye çevir
                string statusText = newStatus switch
                {
                    OrderStatus.Pending => "Beklemede",
                    OrderStatus.Confirmed => "Onaylandı",
                    OrderStatus.Shipped => "Kargoya Verildi",
                    OrderStatus.Delivered => "Teslim Edildi",
                    OrderStatus.Cancelled => "İptal Edildi",
                    OrderStatus.Returned => "İade Edildi",
                    _ => "Bilinmeyen"
                };

                return Json(new { 
                    success = true, 
                    message = $"Sipariş durumu '{statusText}' olarak güncellendi.",
                    newStatus = (int)newStatus,
                    statusText = statusText
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Hata: {ex.Message}" });
            }
        }

        /// <summary>
        /// Sipariş İptal Et
        /// POST: /Admin/Order/Cancel
        /// </summary>
        [HttpPost]
        public IActionResult Cancel(int id)
        {
            try
            {
                var order = _orderService.GetById(id);
                
                if (order == null)
                {
                    TempData["Error"] = "Sipariş bulunamadı!";
                    return RedirectToAction("Index");
                }

                // Durumu Cancelled yap
                _orderService.UpdateStatus(id, OrderStatus.Cancelled, "Admin");

                TempData["Success"] = $"Sipariş #{id} iptal edildi.";
                return RedirectToAction("Details", new { id });
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Hata: {ex.Message}";
                return RedirectToAction("Details", new { id });
            }
        }

        /// <summary>
        /// Sipariş Sil (Soft Delete)
        /// POST: /Admin/Order/Delete
        /// </summary>
        [HttpPost]
        public IActionResult Delete(int id)
        {
            try
            {
                var order = _orderService.GetById(id);
                
                if (order == null)
                {
                    return Json(new { success = false, message = "Sipariş bulunamadı!" });
                }

                // Soft delete
                _orderService.Delete(id, "Admin");

                return Json(new { 
                    success = true, 
                    message = $"Sipariş #{id} silindi." 
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Hata: {ex.Message}" });
            }
        }

        /// <summary>
        /// Toplu Sipariş Durumu Değiştir (AJAX)
        /// POST: /Admin/Order/BulkUpdateStatus
        /// </summary>
        [HttpPost]
        public IActionResult BulkUpdateStatus([FromBody] BulkStatusUpdateModel model)
        {
            try
            {
                if (model.OrderIds == null || !model.OrderIds.Any())
                {
                    return Json(new { success = false, message = "Sipariş seçilmedi!" });
                }

                var newStatus = (OrderStatus)model.Status;
                
                foreach (var orderId in model.OrderIds)
                {
                    _orderService.UpdateStatus(orderId, newStatus, "Admin");
                }

                return Json(new { 
                    success = true, 
                    message = $"{model.OrderIds.Count} sipariş güncellendi." 
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Hata: {ex.Message}" });
            }
        }
    }

    /// <summary>
    /// Toplu durum güncelleme için model
    /// </summary>
    public class BulkStatusUpdateModel
    {
        public List<int> OrderIds { get; set; } = new();
        public int Status { get; set; }
    }
}

