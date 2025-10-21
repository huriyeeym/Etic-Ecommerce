using Etic.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etic.Entities
{
    /// <summary>
    /// Ayarlar tablosu (Key-Value store)
    /// </summary>
    public class Setting : IEntity
    {
        /// <summary>
        /// Ayar anahtarı (örn: SiteName, Logo, Email)
        /// ZORUNLU - Primary Key
        /// </summary>
        public string Id { get; set; } = string.Empty;
        
        /// <summary>
        /// Ayar değeri - OPSIYONEL (boş olabilir)
        /// </summary>
        public string? SettingValue { get; set; }
    }
}
