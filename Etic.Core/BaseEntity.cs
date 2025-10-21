using System;

namespace Etic.Core
{
    /// <summary>
    /// Tüm entity'lerin ortak özelliklerini içeren temel sınıf
    /// </summary>
    public abstract class BaseEntity : IEntity
    {
        // ============================================
        // PRIMARY KEY
        // ============================================
        
        /// <summary>
        /// Benzersiz kimlik numarası (Primary Key)
        /// </summary>
        public int Id { get; set; }

        // ============================================
        // AUDIT KOLONLARI (İzlenebilirlik)
        // ============================================
        
        /// <summary>
        /// Kayıt ne zaman oluşturuldu?
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Kayıt en son ne zaman güncellendi?
        /// </summary>
        public DateTime? UpdatedDate { get; set; }
        // ☝️ Nullable (?) çünkü güncellenmemiş olabilir

        /// <summary>
        /// Kim oluşturdu? (Kullanıcı ID veya Email)
        /// </summary>
        public string? CreatedBy { get; set; }

        /// <summary>
        /// Kim güncelledi?
        /// </summary>
        public string? UpdatedBy { get; set; }

        // ============================================
        // SOFT DELETE (Yumuşak Silme)
        // ============================================
        
        /// <summary>
        /// Kayıt silinmiş mi? (True = silinmiş, False = aktif)
        /// Gerçekten silmiyoruz, sadece işaretliyoruz!
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Ne zaman silindi?
        /// </summary>
        public DateTime? DeletedDate { get; set; }

        /// <summary>
        /// Kim sildi?
        /// </summary>
        public string? DeletedBy { get; set; }
    }
}

