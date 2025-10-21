using Etic.Core;

namespace Etic.Entities
{
    /// <summary>
    /// Kullanıcı adresi entity'si
    /// </summary>
    public class UserAddress : BaseEntity
    {
        // ============================================
        // KULLANICI BİLGİSİ
        // ============================================
        
        /// <summary>
        /// Hangi kullanıcının adresi?
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Kullanıcı bilgisi (ZORUNLU - Her adresin sahibi olmalı)
        /// </summary>
        public virtual User User { get; set; } = null!;

        // ============================================
        // ADRES BİLGİLERİ
        // ============================================
        
        /// <summary>
        /// Adres başlığı (örn: Ev, İş, Ofis) - OPSIYONEL
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// Adresin sahibinin adı (ZORUNLU)
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Adresin sahibinin soyadı (ZORUNLU)
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Açık adres (ZORUNLU)
        /// </summary>
        public string Address { get; set; } = string.Empty;

        /// <summary>
        /// İlçe (ZORUNLU)
        /// </summary>
        public string District { get; set; } = string.Empty;

        /// <summary>
        /// İl / Şehir (ZORUNLU)
        /// </summary>
        public string City { get; set; } = string.Empty;

        /// <summary>
        /// Posta kodu (OPSIYONEL)
        /// </summary>
        public string? PostalCode { get; set; }

        /// <summary>
        /// Telefon numarası (ZORUNLU)
        /// </summary>
        public string PhoneNumber { get; set; } = string.Empty;

        /// <summary>
        /// Email (Opsiyonel)
        /// </summary>
        public string? Email { get; set; }

        // ============================================
        // DURUM BİLGİLERİ
        // ============================================
        
        /// <summary>
        /// Varsayılan adres mi? (True = varsayılan)
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// Fatura adresi mi?
        /// </summary>
        public bool IsBillingAddress { get; set; }

        /// <summary>
        /// Teslimat adresi mi?
        /// </summary>
        public bool IsShippingAddress { get; set; }
    }
}
