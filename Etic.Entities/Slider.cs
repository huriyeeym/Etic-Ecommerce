using Etic.Core;

namespace Etic.Entities
{
    /// <summary>
    /// Slider (Anasayfa kaydırıcı) entity'si
    /// </summary>
    public class Slider : BaseEntity
    {
        // ============================================
        // TEMEL BİLGİLER
        // ============================================
        
        /// <summary>
        /// Slider adı (Yönetim paneli için)
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Başlık (Büyük metin)
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// Alt başlık / Açıklama (Küçük metin)
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Resim URL'i (örn: /images/slider1.jpg)
        /// </summary>
        public string ImageUrl { get; set; } = string.Empty;

        /// <summary>
        /// Link (Tıklanınca nereye gitsin?)
        /// </summary>
        public string? Link { get; set; }

        /// <summary>
        /// Sıralama numarası
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// Aktif mi? (True = göster, False = gizle)
        /// </summary>
        public bool IsActive { get; set; }
    }
}
