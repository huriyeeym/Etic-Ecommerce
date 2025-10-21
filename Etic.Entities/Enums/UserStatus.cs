namespace Etic.Entities.Enums
{
    /// <summary>
    /// Kullanıcı durumu
    /// </summary>
    public enum UserStatus
    {
        /// <summary>
        /// Pasif - Kullanıcı hesabı kapalı
        /// </summary>
        Inactive = 0,
        // ☝️ 0 = false gibi düşün

        /// <summary>
        /// Aktif - Kullanıcı giriş yapabilir
        /// </summary>
        Active = 1,
        // ☝️ 1 = true gibi düşün

        /// <summary>
        /// Yasaklı - Kullanıcı engellenmiş
        /// </summary>
        Banned = 2,

        /// <summary>
        /// Beklemede - Email onayı bekleniyor
        /// </summary>
        PendingVerification = 3
    }
}

