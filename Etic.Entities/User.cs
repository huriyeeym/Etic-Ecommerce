using Etic.Core;
using Etic.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Etic.Entities
{
    /// <summary>
    /// Kullanıcı entity'si
    /// </summary>
    public class User : BaseEntity
    {
        // ============================================
        // KİŞİSEL BİLGİLER
        // ============================================
        
        /// <summary>
        /// Kullanıcının adı
        /// </summary>
        [Required(ErrorMessage = "Ad alanı zorunludur")]
        [StringLength(100, ErrorMessage = "Ad en fazla 100 karakter olabilir")]
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Kullanıcının soyadı
        /// </summary>
        [Required(ErrorMessage = "Soyad alanı zorunludur")]
        [StringLength(100, ErrorMessage = "Soyad en fazla 100 karakter olabilir")]
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Tam isim (FirstName + LastName)
        /// Geriye dönük uyumluluk için tutuyoruz
        /// </summary>
        public string FullName { get; set; } = string.Empty;

        /// <summary>
        /// Telefon numarası
        /// </summary>
        public string? Phone { get; set; }

        // ============================================
        // GİRİŞ BİLGİLERİ
        // ============================================
        
        /// <summary>
        /// Email adresi (Unique olmalı!)
        /// </summary>
        [Required(ErrorMessage = "Email adresi zorunludur")]
        [EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz")]
        [StringLength(200, ErrorMessage = "Email en fazla 200 karakter olabilir")]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Şifre hash'i (DÜZ ŞİFRE DEĞİL!)
        /// SHA256 ile hash'lenmiş olmalı
        /// </summary>
        [Required]
        [StringLength(500)]
        public string PasswordHash { get; set; } = string.Empty;

        /// <summary>
        /// Otomatik giriş için GUID key
        /// </summary>
        public string? LoginGuidKey { get; set; }

        // ============================================
        // DURUM BİLGİLERİ
        // ============================================
        
        /// <summary>
        /// Kullanıcı durumu (Active, Inactive, Banned, PendingVerification)
        /// </summary>
        public UserStatus Status { get; set; }

        /// <summary>
        /// Admin mi? (True = Admin, False = Normal kullanıcı)
        /// </summary>
        public bool IsAdmin { get; set; }

        /// <summary>
        /// Kayıt tarihi
        /// </summary>
        public DateTime RegisterDate { get; set; }

        // ============================================
        // NAVİGATION PROPERTİES (İlişkiler)
        // ============================================
        
        /// <summary>
        /// Kullanıcının adresleri (1 kullanıcının birden fazla adresi olabilir)
        /// </summary>
        public virtual ICollection<UserAddress> Addresses { get; set; } = new List<UserAddress>();

        /// <summary>
        /// Kullanıcının siparişleri
        /// </summary>
        public virtual ICollection<Orders> Orders { get; set; } = new List<Orders>();

        /// <summary>
        /// Kullanıcının sepeti
        /// </summary>
        public virtual Basket Basket { get; set; } = null!;
    }
}
