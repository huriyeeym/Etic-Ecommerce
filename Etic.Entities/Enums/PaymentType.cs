namespace Etic.Entities.Enums
{
    /// <summary>
    /// Ödeme tipi
    /// </summary>
    public enum PaymentType
    {
        /// <summary>
        /// Kapıda ödeme (Nakit veya Kredi Kartı)
        /// </summary>
        CashOnDelivery = 1,

        /// <summary>
        /// Kredi kartı ile online ödeme
        /// </summary>
        CreditCard = 2,

        /// <summary>
        /// Havale/EFT
        /// </summary>
        BankTransfer = 3,

        /// <summary>
        /// İyzico ile ödeme
        /// </summary>
        Iyzico = 4
    }
}

