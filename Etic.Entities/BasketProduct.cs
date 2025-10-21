using Etic.Core;
using System;

namespace Etic.Entities
{
    /// <summary>
    /// Sepet-Ürün ilişki tablosu
    /// </summary>
    public class BasketProduct : BaseEntity
    {
        // ============================================
        // İLİŞKİ BİLGİLERİ
        // ============================================
        
        /// <summary>
        /// Hangi sepet?
        /// </summary>
        public Guid BasketId { get; set; }

        /// <summary>
        /// Sepet bilgisi (Navigation Property)
        /// </summary>
        public virtual Basket Basket { get; set; } = null!;

        /// <summary>
        /// Hangi ürün?
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Ürün bilgisi (Navigation Property)
        /// </summary>
        public virtual Product Product { get; set; } = null!;

        // ============================================
        // MİKTAR VE TARİH
        // ============================================
        
        /// <summary>
        /// Kaç adet?
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Ne zaman eklendi?
        /// </summary>
        public DateTime AddDate { get; set; }

        /// <summary>
        /// Durum (True = sepette, False = silindi)
        /// </summary>
        public bool? Status { get; set; }
    }
}
