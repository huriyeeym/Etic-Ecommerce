using Etic.Core;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Etic.Entities
{
    /// <summary>
    /// Kategori entity'si
    /// </summary>
    public class Category : BaseEntity
    {
        // ============================================
        // TEMEL BİLGİLER
        // ============================================
        
        /// <summary>
        /// Kategori adı (örn: Elektronik, Bilgisayar)
        /// </summary>
        [Required(ErrorMessage = "Kategori adı zorunludur")]
        [StringLength(100, ErrorMessage = "Kategori adı en fazla 100 karakter olabilir")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// SEO dostu URL (örn: elektronik)
        /// </summary>
        [Required]
        [StringLength(200)]
        public string SeoLink { get; set; } = string.Empty;

        /// <summary>
        /// Sıralama numarası (Menüde hangi sırada görünsün?)
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// İkon ismi (örn: fa-laptop, fa-phone)
        /// </summary>
        public string? IconName { get; set; }

        /// <summary>
        /// Link (Opsiyonel özel link)
        /// </summary>
        public string? Link { get; set; }

        // ============================================
        // HİYERARŞİK YAPI (Alt Kategoriler)
        // ============================================
        
        /// <summary>
        /// Üst kategori ID (Null ise ana kategori)
        /// Örnek: Laptop kategorisinin üst kategorisi Bilgisayar
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// Üst kategori (Navigation Property)
        /// </summary>
        public virtual Category? ParentCategory { get; set; }

        /// <summary>
        /// Alt kategoriler (Navigation Property)
        /// </summary>
        public virtual ICollection<Category> ChildCategories { get; set; } = new List<Category>();

        // ============================================
        // NAVİGATION PROPERTİES (İlişkiler)
        // ============================================
        
        /// <summary>
        /// Bu kategorideki ürünler (Many-to-Many ilişki)
        /// </summary>
        public virtual ICollection<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();
    }
}
