using System;
using System.Collections.Generic;
using System.Linq;
using Etic.Data.Abstract;
using Etic.Entities;
using Etic.Entities.Enums;

namespace Etic.Business.Services
{
    public class OrderService : IOrderService
    {
        // Data Access Layer'ı kullanacağız (veritabanı işlemleri için)
        private readonly IOrdersDal _ordersDal;
        private readonly IOrderProductsDal _orderProductsDal;

        // Constructor: Dependency Injection ile dal'ları alıyoruz
        public OrderService(IOrdersDal ordersDal, IOrderProductsDal orderProductsDal)
        {
            _ordersDal = ordersDal;
            _orderProductsDal = orderProductsDal;
        }

        /// <summary>
        /// Tüm siparişleri getir (silinmemiş olanlar)
        /// </summary>
        public IList<Orders> GetAll()
        {
            return _ordersDal.GetAll(x => !x.IsDeleted);
        }

        /// <summary>
        /// ID'ye göre sipariş getir
        /// </summary>
        public Orders GetById(int orderId)
        {
            return _ordersDal.Get(x => x.Id == orderId && !x.IsDeleted);
        }

        /// <summary>
        /// Belirli bir kullanıcının siparişlerini getir
        /// </summary>
        public IList<Orders> GetByUserId(int userId)
        {
            return _ordersDal.GetAll(x => x.UserId == userId && !x.IsDeleted);
        }

        /// <summary>
        /// Duruma göre siparişleri filtrele
        /// Örnek: GetByStatus(OrderStatus.Pending) → Bekleyen siparişler
        /// </summary>
        public IList<Orders> GetByStatus(OrderStatus status)
        {
            return _ordersDal.GetAll(x => x.Status == status && !x.IsDeleted);
        }

        /// <summary>
        /// Yeni sipariş ekle
        /// </summary>
        public void Add(Orders order)
        {
            order.CreatedDate = DateTime.Now;
            order.CreateDate = DateTime.Now;
            _ordersDal.Add(order);
        }

        /// <summary>
        /// Sipariş güncelle
        /// </summary>
        public void Update(Orders order)
        {
            order.UpdatedDate = DateTime.Now;
            _ordersDal.Update(order);
        }

        /// <summary>
        /// Sipariş durumunu değiştir
        /// Bu metot özel: Admin panelde kullanılacak
        /// </summary>
        public void UpdateStatus(int orderId, OrderStatus newStatus, string? updatedBy = null)
        {
            var order = GetById(orderId);
            if (order != null)
            {
                order.Status = newStatus;
                order.UpdatedDate = DateTime.Now;
                order.UpdatedBy = updatedBy;
                _ordersDal.Update(order);
            }
        }

        /// <summary>
        /// Sipariş sil (Soft Delete)
        /// Gerçekten silmiyoruz, IsDeleted = true yapıyoruz
        /// </summary>
        public void Delete(int orderId, string? deletedBy = null)
        {
            var order = GetById(orderId);
            if (order != null)
            {
                order.IsDeleted = true;
                order.DeletedDate = DateTime.Now;
                order.DeletedBy = deletedBy;
                _ordersDal.Update(order);
            }
        }

        /// <summary>
        /// Toplam sipariş sayısı
        /// </summary>
        public int GetTotalCount()
        {
            return _ordersDal.GetAll(x => !x.IsDeleted).Count;
        }

        /// <summary>
        /// Belirli bir durumdaki sipariş sayısı
        /// Dashboard için kullanılacak
        /// </summary>
        public int GetCountByStatus(OrderStatus status)
        {
            return _ordersDal.GetAll(x => x.Status == status && !x.IsDeleted).Count;
        }

        /// <summary>
        /// Son X adet siparişi getir (Dashboard için)
        /// </summary>
        public IList<Orders> GetLatestOrders(int count = 10)
        {
            return _ordersDal.GetAll(x => !x.IsDeleted)
                .OrderByDescending(x => x.CreateDate)
                .Take(count)
                .ToList();
        }

        /// <summary>
        /// Toplam gelir hesapla
        /// Sadece Delivered (Teslim edilmiş) siparişlerden
        /// </summary>
        public decimal GetTotalRevenue()
        {
            return _ordersDal.GetAll(x => x.Status == OrderStatus.Delivered && !x.IsDeleted)
                .Sum(x => x.TotalAmount);
        }

        /// <summary>
        /// Bugünkü siparişler
        /// </summary>
        public IList<Orders> GetTodayOrders()
        {
            var today = DateTime.Today;
            return _ordersDal.GetAll(x => 
                x.CreateDate.Date == today && !x.IsDeleted);
        }

        /// <summary>
        /// Bu ay'ın siparişleri
        /// </summary>
        public IList<Orders> GetThisMonthOrders()
        {
            var now = DateTime.Now;
            var firstDayOfMonth = new DateTime(now.Year, now.Month, 1);
            return _ordersDal.GetAll(x => 
                x.CreateDate >= firstDayOfMonth && !x.IsDeleted);
        }

        /// <summary>
        /// Siparişteki ürünleri getir
        /// </summary>
        public IList<OrderProducts> GetOrderProducts(int orderId)
        {
            return _orderProductsDal.GetAll(x => x.OrderId == orderId);
        }
    }

    /// <summary>
    /// OrderService interface'i
    /// Dependency Injection için kullanılacak
    /// </summary>
    public interface IOrderService
    {
        IList<Orders> GetAll();
        Orders GetById(int orderId);
        IList<Orders> GetByUserId(int userId);
        IList<Orders> GetByStatus(OrderStatus status);
        void Add(Orders order);
        void Update(Orders order);
        void UpdateStatus(int orderId, OrderStatus newStatus, string? updatedBy = null);
        void Delete(int orderId, string? deletedBy = null);
        int GetTotalCount();
        int GetCountByStatus(OrderStatus status);
        IList<Orders> GetLatestOrders(int count = 10);
        decimal GetTotalRevenue();
        IList<Orders> GetTodayOrders();
        IList<Orders> GetThisMonthOrders();
        IList<OrderProducts> GetOrderProducts(int orderId);
    }
}

