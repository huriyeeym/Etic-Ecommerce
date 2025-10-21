namespace Etic.Entities.Enums
{
    /// <summary>
    /// Sipariş durumu
    /// </summary>
    public enum OrderStatus
    {
        /// <summary>
        /// Beklemede - Sipariş alındı ama işleme başlanmadı
        /// </summary>
        Pending = 1,

        /// <summary>
        /// Onaylandı - Sipariş onaylandı, hazırlanıyor
        /// </summary>
        Confirmed = 2,

        /// <summary>
        /// Kargoya verildi
        /// </summary>
        Shipped = 3,

        /// <summary>
        /// Teslim edildi
        /// </summary>
        Delivered = 4,

        /// <summary>
        /// İptal edildi
        /// </summary>
        Cancelled = 5,

        /// <summary>
        /// İade edildi
        /// </summary>
        Returned = 6
    }
}

