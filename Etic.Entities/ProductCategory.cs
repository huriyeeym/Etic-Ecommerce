using Etic.Core;
using System.ComponentModel.DataAnnotations;

namespace Etic.Entities
{
    /// <summary>
    /// Ürün-Kategori ilişki tablosu (Many-to-Many)
    /// </summary>
    public class ProductCategory : IEntity
    {
        // ============================================
        // COMPOSITE PRIMARY KEY
        // ============================================
        
        /// <summary>
        /// Ürün ID (Composite Key'in bir parçası)
        /// </summary>
        [Key]
        public int ProductId { get; set; }

        /// <summary>
        /// Kategori ID (Composite Key'in bir parçası)
        /// </summary>
        [Key]
        public int CategoryId { get; set; }

        // ============================================
        // EK BİLGİLER
        // ============================================
        
        /// <summary>
        /// Kategori içinde sıralama (Opsiyonel)
        /// </summary>
        public int? Sort { get; set; }

        // ============================================
        // NAVİGATION PROPERTİES (İlişkiler)
        // ============================================
        
        /// <summary>
        /// Ürün bilgisi (ZORUNLU - Her ProductCategory'nin ürünü olmalı)
        /// </summary>
        public virtual Product Product { get; set; } = null!;

        /// <summary>
        /// Kategori bilgisi (ZORUNLU - Her ProductCategory'nin kategorisi olmalı)
        /// </summary>
        public virtual Category Category { get; set; } = null!;
    }
}
