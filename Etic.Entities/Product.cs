using Etic.Core;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Etic.Entities
{
    /// <summary>
    /// Ürün entity'si
    /// </summary>
    public class Product : BaseEntity
    {
        // ============================================
        // TEMEL BİLGİLER
        // ============================================
        
        /// <summary>
        /// Ürün adı
        /// </summary>
        [Required(ErrorMessage = "Ürün adı zorunludur")]
        [StringLength(200, ErrorMessage = "Ürün adı en fazla 200 karakter olabilir")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Kısa açıklama (liste görünümü için)
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Detaylı ürün açıklaması
        /// </summary>
        public string? ProductDescription { get; set; }

        /// <summary>
        /// SEO dostu URL (örn: iphone-14-pro)
        /// </summary>
        public string SeoLink { get; set; } = string.Empty;

        // ============================================
        // FİYAT VE STOK
        // ============================================
        
        /// <summary>
        /// Ürün fiyatı (TL)
        /// </summary>
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Fiyat 0'dan büyük olmalıdır")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        /// <summary>
        /// Stok adedi
        /// </summary>
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Stok negatif olamaz")]
        public int Stock { get; set; }

        /// <summary>
        /// Satışta mı? (True = satışta, False = satışta değil)
        /// </summary>
        public bool IsActive { get; set; }

        // ============================================
        // MARKA BİLGİSİ
        // ============================================
        
        /// <summary>
        /// Marka ID (Opsiyonel)
        /// TODO: Brand entity'si oluşturulacak
        /// </summary>
        public int? BrandId { get; set; }

        // Navigation property (gelecekte eklenecek):
        // public virtual Brand Brand { get; set; }

        // ============================================
        // NAVİGATION PROPERTİES (İlişkiler)
        // ============================================
        
        /// <summary>
        /// Ürünün görselleri (1 ürünün birden fazla görseli olabilir)
        /// </summary>
        public virtual ICollection<ProductImage> Images { get; set; } = new List<ProductImage>();

        /// <summary>
        /// Ürünün kategorileri (Many-to-Many ilişki)
        /// </summary>
        public virtual ICollection<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();

        /// <summary>
        /// Bu ürün hangi siparişlerde var?
        /// </summary>
        public virtual ICollection<OrderProducts> OrderProducts { get; set; } = new List<OrderProducts>();

        /// <summary>
        /// Bu ürün hangi sepetlerde var?
        /// </summary>
        public virtual ICollection<BasketProduct> BasketProducts { get; set; } = new List<BasketProduct>();
    }
}
