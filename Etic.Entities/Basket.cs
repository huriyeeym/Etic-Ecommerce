using Etic.Core;
using System;
using System.Collections.Generic;

namespace Etic.Entities
{
    /// <summary>
    /// Sepet entity'si
    /// </summary>
    public class Basket : IEntity
    {
        // ============================================
        // PRIMARY KEY (GUID kullanıyoruz!)
        // ============================================
        
        /// <summary>
        /// Sepet ID (GUID - Benzersiz kimlik)
        /// 
        /// NEDEN GUID?
        /// - Güvenlik: Sepet ID'leri tahmin edilemez (1,2,3... gibi sıralı değil)
        /// - Başkasının sepetine erişim engellenmiş olur
        /// - URL'de sepet ID'si gösterildiğinde güvenli
        /// 
        /// DİĞER TABLOLAR INT: Basket hariç tüm tablolarda INT kullanılmış.
        /// Bu bilinçli bir tasarım kararıdır (güvenlik vs performans trade-off)
        /// </summary>
        public Guid Id { get; set; }

        // ============================================
        // KULLANICI BİLGİSİ
        // ============================================
        
        /// <summary>
        /// Hangi kullanıcının sepeti?
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Kullanıcı bilgisi (Navigation Property)
        /// </summary>
        public virtual User User { get; set; } = null!;

        // ============================================
        // TARİH BİLGİLERİ
        // ============================================
        
        /// <summary>
        /// Sepet ne zaman oluşturuldu?
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Sepet siparişe çevrildi mi? Ne zaman?
        /// </summary>
        public DateTime? OrderDate { get; set; }

        /// <summary>
        /// Hangi siparişe çevrildi? (Opsiyonel)
        /// </summary>
        public int? OrderId { get; set; }

        /// <summary>
        /// Sipariş bilgisi (Navigation Property)
        /// </summary>
        public virtual Orders? Order { get; set; }

        // ============================================
        // DURUM BİLGİSİ
        // ============================================
        
        /// <summary>
        /// Sepet durumu (True = aktif, False = siparişe çevrildi)
        /// </summary>
        public bool? Status { get; set; }

        // ============================================
        // NAVİGATION PROPERTİES (İlişkiler)
        // ============================================
        
        /// <summary>
        /// Sepetteki ürünler
        /// </summary>
        public virtual ICollection<BasketProduct> BasketProducts { get; set; } = new List<BasketProduct>();
    }
}
