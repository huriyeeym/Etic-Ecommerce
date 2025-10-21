using Etic.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etic.Entities
{
    /// <summary>
    /// SQL View - Sepet ve ürün bilgilerini birleştirir
    /// </summary>
    public class VwBasketProductList : IEntity
    {
        public int Id { get; set; }
        public Guid BasketId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime AddDate { get; set; }
        public bool? Status { get; set; }
        
        /// <summary>
        /// Ürün adı (View'dan gelir, asla null değil)
        /// </summary>
        public string ProductName { get; set; } = string.Empty;
        
        /// <summary>
        /// Ürün görseli (View'dan gelir, asla null değil)
        /// </summary>
        public string ProductImage { get; set; } = string.Empty;

        public decimal? Price { get; set; }
    }
}
