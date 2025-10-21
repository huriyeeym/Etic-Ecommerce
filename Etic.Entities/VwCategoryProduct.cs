using Etic.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etic.Entities
{
    /// <summary>
    /// SQL View - Kategori ve ürün bilgilerini birleştirir
    /// </summary>
    public class VwCategoryProduct : IEntity
    {
        public int Id { get; set; }

        /// <summary>
        /// Ürün adı (View'dan gelir, asla null değil)
        /// </summary>
        public string Name { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public int? BrandId { get; set; }

        /// <summary>
        /// SEO Link (View'dan gelir, asla null değil)
        /// </summary>
        public string SeoLink { get; set; } = string.Empty;

        /// <summary>
        /// Ürün açıklaması (View'dan gelir, asla null değil)
        /// </summary>
        public string ProductDescription { get; set; } = string.Empty;

        /// <summary>
        /// Etiketler (View'dan gelir, asla null değil)
        /// </summary>
        public string Tags { get; set; } = string.Empty;

        public int CategoryId { get; set; }

        public int? Sort { get; set; }

        /// <summary>
        /// Kategori SEO Link (View'dan gelir, asla null değil)
        /// </summary>
        public string SeoLinkCat { get; set; } = string.Empty;

        /// <summary>
        /// Görsel URL (View'dan gelir, asla null değil)
        /// </summary>
        public string ImageUrl { get; set; } = string.Empty;

    }
}
