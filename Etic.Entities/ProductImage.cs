using Etic.Core;

namespace Etic.Entities
{
    /// <summary>
    /// Ürün görseli entity'si
    /// </summary>
    public class ProductImage : BaseEntity
    {
        // ============================================
        // İLİŞKİ BİLGİSİ
        // ============================================
        
        /// <summary>
        /// Hangi ürünün görseli?
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Ürün bilgisi (ZORUNLU - Her görselin ürünü olmalı)
        /// </summary>
        public virtual Product Product { get; set; } = null!;

        // ============================================
        // GÖRSEL BİLGİLERİ
        // ============================================
        
        /// <summary>
        /// Görsel URL'i (örn: /images/products/iphone-14-pro.jpg)
        /// </summary>
        public string ImageUrl { get; set; } = string.Empty;

        /// <summary>
        /// Sıralama (Hangi görsel önce görünsün?)
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// Ana görsel mi? (True = ana görsel, False = ek görsel)
        /// </summary>
        public bool IsMainImage { get; set; }
    }
}
