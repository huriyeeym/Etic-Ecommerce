using Etic.Core;
using Etic.Entities.Enums;
using System;
using System.Collections.Generic;

namespace Etic.Entities
{
    /// <summary>
    /// Sipariş entity'si
    /// </summary>
    public class Orders : BaseEntity
    {
        // ============================================
        // KULLANICI BİLGİSİ
        // ============================================
        
        /// <summary>
        /// Hangi kullanıcının siparişi?
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Kullanıcı bilgisi (Navigation Property)
        /// </summary>
        public virtual User User { get; set; } = null!;

        // ============================================
        // SİPARİŞ BİLGİLERİ
        // ============================================
        
        /// <summary>
        /// Sipariş oluşturulma tarihi
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Ödeme tipi (CashOnDelivery, CreditCard, BankTransfer, Iyzico)
        /// </summary>
        public PaymentType? PaymentType { get; set; }

        /// <summary>
        /// Sipariş durumu (Pending, Confirmed, Shipped, Delivered, Cancelled, Returned)
        /// </summary>
        public OrderStatus Status { get; set; }

        /// <summary>
        /// Toplam tutar
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Sipariş notu (Kullanıcıdan)
        /// </summary>
        public string? OrderNote { get; set; }

        /// <summary>
        /// Teslimat adresi ID
        /// </summary>
        public int? DeliveryAddressId { get; set; }

        /// <summary>
        /// Teslimat adresi (Navigation Property)
        /// </summary>
        public virtual UserAddress? DeliveryAddress { get; set; }

        // ============================================
        // NAVİGATION PROPERTİES (İlişkiler)
        // ============================================
        
        /// <summary>
        /// Siparişteki ürünler
        /// </summary>
        public virtual ICollection<OrderProducts> OrderProducts { get; set; } = new List<OrderProducts>();
    }
}
