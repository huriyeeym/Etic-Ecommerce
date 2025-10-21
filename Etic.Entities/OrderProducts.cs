using Etic.Core;

namespace Etic.Entities
{
    /// <summary>
    /// Sipariş-Ürün ilişki tablosu
    /// </summary>
    public class OrderProducts : BaseEntity
    {
        // ============================================
        // İLİŞKİ BİLGİLERİ
        // ============================================
        
        /// <summary>
        /// Hangi sipariş?
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Sipariş bilgisi (Navigation Property)
        /// </summary>
        public virtual Orders Order { get; set; } = null!;

        /// <summary>
        /// Hangi ürün?
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Ürün bilgisi (Navigation Property)
        /// </summary>
        public virtual Product Product { get; set; } = null!;

        // ============================================
        // MİKTAR VE FİYAT
        // ============================================
        
        /// <summary>
        /// Kaç adet?
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Sipariş anındaki birim fiyat
        /// (Ürün fiyatı değişse bile siparişteki fiyat sabit kalır!)
        /// </summary>
        public decimal Price { get; set; }
    }
}
